using System;
using System.ComponentModel.DataAnnotations;

namespace OnTheFly_Final.Models
{
    public class Company
    {

        //public string Id { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório!"), StringLength(19, ErrorMessage = "CNPJ inválido!")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório!"), StringLength(30, ErrorMessage = "Nome inválido!")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Este campo é obrigatório!"), StringLength(30, ErrorMessage = "Nome inválido!")]
        public string NameOpt { get; set; } //se nao for informado, colocar o msm que o name

        [Required(ErrorMessage = "Este campo é obrigatório!")]
        public DateTime DtOpen { get; set; }
        public bool ? Status { get; set; } //nullable

        //[Required(ErrorMessage = "Este campo é obrigatório!")]
        //public Address Address { get; set; }

    }
}
