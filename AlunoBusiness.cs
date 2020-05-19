using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System;

namespace csharp_console
{
    partial class Program
    {
        static List<Aluno> alunos = new List<Aluno>();

        static ConsoleKeyInfo ConsoleKeyInfo;

        static bool continuarOp = false;

        private static void CalcularMediaGeral()
        {
            if (alunos.Any())
            {
                var mediaGeral = alunos.Average(x => x.Nota);

                var conceito = GetConceitoNota(mediaGeral);

                /*  teste uso Lista de enum*/
                // var lstConceito = Enum.GetValues(typeof(Conceito)).OfType<Conceito>();

                // var conceito = lstConceito.Where(w=>w.GetHashCode() > (int) mediaGeral)
                //                             .Select(s => s.ToString()).SingleOrDefault();

                Console.WriteLine(MsgConsole.MSG11(mediaGeral, conceito));
                Console.WriteLine();
            }
            else
                Console.WriteLine(MsgConsole.MSG01);
        }

        private static string GetConceitoNota(decimal media)
        {
            string conceito = ConceitoNota.A.ToString();
            
            foreach (var conc in Enum.GetValues(typeof(ConceitoNota)))
            {
                //Altera o conceito mediante a nota vinculada ao ENUM
                if (media < conc.GetHashCode())
                    conceito = conc.ToString();
                break;
            }

            return conceito;
        }

        private static void ListarAluno()
        {
            if (alunos.Any()){
                Console.WriteLine("Lançamentos efetuados:");
                foreach (var a in alunos.Where(x => !String.IsNullOrEmpty(x.Nome)))
                    Console.WriteLine(a);
            }
            else
                Console.WriteLine(MsgConsole.MSG01);
        }

        private static void InserirAluno()
        {
            try
            {
                do
                {
                    //passando id 0 fará busca do último id inserido
                    PersisteIncluirAlterar(new Aluno(0));
                    continuarOp = ProsseguirOperacao(MsgConsole.MSG12(CRUD.inserir));
                } while (continuarOp);
            }
            catch (Exception)
            {
                throw new ArgumentException(MsgConsole.MSG04(CRUD.inserir));
            }
        }

        //Validações de parâmetros de campos em DataAnotation e de Console
        //retorna confirmação upd/insert 
        private static void PersisteIncluirAlterar(Aluno aluno)
        {           
            Console.Write(MsgConsole.MSG02(aluno.Nome));
            aluno.Nome = Console.ReadLine();
            Console.WriteLine();

            //Tratativa para mensagem da atualização que exive o valor anterior
            Console.Write(MsgConsole.MSG03(aluno.ID==0 ? "" : aluno.ID.ToString()));
            
            //Caso não receba valor numérico, atribui 999 que está fora dos limites DataAnnotations
            aluno.Nota = decimal.TryParse(Console.ReadLine(), out decimal nota) ? nota : 999;            
            Console.WriteLine();
        
            //Consiste propriedades Aluno
            var erros = Util.getValidationErros(aluno);            

            //Não retornando erros, adicionar cadastro
            if (erros.Count().Equals(0))
            {
                if (ProsseguirOperacao(
                        MsgConsole.MSG10(aluno.ID == 0 ? CRUD.inserir:CRUD.alterar)))
                {
                    if (aluno.ID==0){
                        //Em add Busca ultimo id, caso não exista, inicializa
                        var novoId = alunos.Any() ? alunos.Select(s=>s.ID).Max() : 1;
                        alunos.Add(new Aluno(novoId){Nome=aluno.Nome, Nota=aluno.Nota});
                        
                        Console.WriteLine(MsgConsole.MSG07(CRUD.inserir));
                    }
                    else{
                        alunos[alunos.IndexOf(aluno)] = aluno; 
                        Console.WriteLine(MsgConsole.MSG07(CRUD.inserir));
                    }
                }
                else
                    Console.WriteLine(MsgConsole.MSG08(aluno.ID == 0 ? CRUD.inserir:CRUD.alterar));

            }
            else
                foreach (var err in erros)
                    Console.WriteLine(err.ErrorMessage);
            
            Console.WriteLine();
        }

        //Criada unicidade com o id, são exibidos vários cadastros para o mesmo nome
        private static void AlterarAluno()
        {
            try
            {
                if (alunos.Any())
                {
                    do
                    {
                        Console.WriteLine(MsgConsole.MSG05(CRUD.alterar));
                        var nomeAlu = Console.ReadLine();
                        
                        //Pesquisa alunos cadastrado com o nome
                        var pesquisaAlu = alunos.Where(w => w.Nome.ToUpper().Equals(nomeAlu.ToUpper())).ToList();
                        
                        var qtdAlu = pesquisaAlu.Count();

                        if (qtdAlu > 0)
                        {
                            Console.WriteLine(MsgConsole.MSG06(qtdAlu));

                            var aluAux = alunos;
                            //Percorre a lista contendo o nome, inclui alterações
                            foreach (var alu in pesquisaAlu)
                            {
                                Console.WriteLine("Cadastro a ser alterado:");
                                Console.WriteLine(alu);
                                Console.WriteLine();

                                PersisteIncluirAlterar(alu);
                            }
                        }
                        else
                            Console.WriteLine(MsgConsole.MSG09(nomeAlu));
        
                        continuarOp = ProsseguirOperacao(MsgConsole.MSG12(CRUD.alterar));
                    } while (continuarOp);
                }
                else
                    Console.WriteLine(MsgConsole.MSG01);
            }
            catch (Exception)
            {
                throw new ArgumentException(MsgConsole.MSG04(CRUD.alterar));
            }
        }

        //A exclusão dos elementos é realizada pelo Nome
        //Existindo mais de um nome, são percorridos e o usuário decide quais realizará exclusão
        private static void ExcluirAluno()
        {
            if (alunos.Any())
            {
                do
                {
                    Console.WriteLine(MsgConsole.MSG05(CRUD.excluir));
                    
                    var nomeAlu = Console.ReadLine();

                    //Pesquisa pelo nome alunos cadastrados
                    var pesquisaAlu = alunos.Where(w => w.Nome.Equals(nomeAlu.ToUpper().ToUpper())).ToList();
                    
                    var qtdAlu = pesquisaAlu.Count();
                    
                    if (qtdAlu > 0)
                    {
                        Console.WriteLine(MsgConsole.MSG06(qtdAlu));
                        foreach(var alu in alunos)
                        {
                            Console.WriteLine(alu);
                            if (ProsseguirOperacao(MsgConsole.MSG10(CRUD.excluir)))
                            {
                                alunos.RemoveAll(r => r.ID == alu.ID);
                                Console.WriteLine(MsgConsole.MSG07(CRUD.excluir));
                            }
                            else
                                Console.WriteLine(MsgConsole.MSG08(CRUD.excluir));
                        }
                    }
                    else
                        Console.WriteLine(MsgConsole.MSG09(nomeAlu));

                    continuarOp = ProsseguirOperacao(MsgConsole.MSG12(CRUD.inserir));

                } while (continuarOp);
            }
            else
                Console.WriteLine(MsgConsole.MSG01);
        }

        private static bool ProsseguirOperacao(string msg)
        {
            do
            {
                Console.WriteLine();
                Console.Write(msg);
                ConsoleKeyInfo = Console.ReadKey();
                Console.WriteLine();
                //Ilustrando condicão do loop percorrendo
                if (ConsoleKeyInfo.Key == ConsoleKey.N)
                    break;

            } while (ConsoleKeyInfo.Key != ConsoleKey.S);
            return ConsoleKeyInfo.Key.Equals(ConsoleKey.S) ? true : false;
        }
    }
} 