
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace csharp_console
{   public class Util
    {
        //Valida propriedades conforme referência em DataAnnotation da classe
        public static IEnumerable<ValidationResult> getValidationErros(object obj)
        {
            var resultadoValidacao = new List<ValidationResult>();
            var contexto = new ValidationContext(obj, null, null);
            Validator.TryValidateObject(obj, contexto, resultadoValidacao , true);
            return resultadoValidacao ;
        }

        //Criar Lista Genérica
        static public List<T> CriaLista<T>(params T[] pars)
        {
            List<T> list = new List<T>();
            foreach (T elem in pars)
                list.Add(elem);
            return list;
        }  
    }
}