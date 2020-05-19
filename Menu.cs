using System;

namespace csharp_console
{
    partial class Program
    {
        public static void ConsoleMode()
        {
            ConsoleKeyInfo opcaoMenu;

            do
            {
                opcaoMenu = ObterOpcaoUsuario();

                switch (opcaoMenu.Key)
                {
                    case ConsoleKey.D1:
                        InserirAluno();
                        break;
                    case ConsoleKey.D2:
                        AlterarAluno();
                        break;                        
                    case ConsoleKey.D3:
                        ExcluirAluno();
                        break;                        
                    case ConsoleKey.D4:
                        ListarAluno();
                        break;
                    case ConsoleKey.D5:
                        CalcularMediaGeral();
                        break;
                    case ConsoleKey.X:
                        break;

                    default: 
                        Console.WriteLine(MsgConsole.MSG14);
                        break;
                }            
            }while(opcaoMenu.Key != ConsoleKey.X);
        }

        private static ConsoleKeyInfo ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("Informe a opção desejada:");
            Console.WriteLine("1- Inserir novo aluno");
            Console.WriteLine("2- Alterar lançamento aluno");
            Console.WriteLine("3- Remover Cadastro");            
            Console.WriteLine("4- Listar alunos");
            Console.WriteLine("5- Calcular média geral");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            var opcaoUsuario = Console.ReadKey();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}