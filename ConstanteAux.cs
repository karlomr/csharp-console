namespace csharp_console
{
    public enum ConceitoNota{ 
        A=10, B=8, C=6, D=4, E=2
    }

    public enum CRUD{
        inserir=1, alterar=2, listar=3, excluir=4
    }

    struct MsgConsole
    {
        public const string MSG01 = "Nenhum registro encontrado!";

        public static string MSG02(string var1){
            return  $"Informe o nome {var1}: ";
        } 
        public static string MSG03(string var1){
           return  $"Informe a Nota {var1}: ";  
        } 

        public static string MSG04(CRUD var1){
            return string.Format("Erro ao {0}!",var1.ToString());
        }

        public static string MSG05(CRUD var1){
            return string.Format("Informe o nome cadastrado para {0} cadastro",var1.ToString());
        }

        public static string MSG06(int var1){
            return $"Cadastros encontrados: {var1}";
        }

        public static string MSG07(CRUD var1){
            return $"Ação {var1} realizada com sucesso!";
        }

        public static string MSG08(CRUD var1){
            return $"Ação {var1} cancelada!";
        }

        public static string MSG09(string var1){
            return $"Nenhum cadastro encontrado com o nome {var1}";
        } 
        public static string MSG10(CRUD var1){
            return string.Format("Confirma {0} o cadastro? S/N ",var1.ToString());
        }
        public static string MSG11(decimal var1, string var2){
            return $"Média Geral: {var1} - Conceito: {var2}";
        }        
        public static string MSG12(CRUD var1){
            return string.Format("Deseja {0} outro cadastro? S/N ",var1.ToString());
        }

        public const string MSG14 = "Informe uma das opções!";
    }

}