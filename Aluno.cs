using System;
using System.ComponentModel.DataAnnotations;

namespace csharp_console
{
    //Testes com dataannotations em console (mensagens em inglês )
    public class Aluno
    {
        public int ID { get;  set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nome é obrigatório!")]
        public string Nome { get; set; }

        [Required]
        [Range(0, 10)]
        [Display(Name = "Informe a nota de 0 a 10")]
        public decimal Nota { get; set; }

        public Aluno(int id){
            ID = id;
        }

        public override string ToString(){
            return $"ID: {ID} | Nome: {Nome} | Nota  {Nota}";
        }
    }
}