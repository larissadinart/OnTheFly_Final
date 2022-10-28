using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnTheFly_Final.Models
{
    [BsonIgnoreExtraElements]
    public class CompanyBlocked
    {
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [StringLength(19, ErrorMessage = "Número de CNPJ inválido")]
        public string CNPJ { get; set; }


        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [StringLength(30, ErrorMessage = "Nome inválido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [StringLength(30, ErrorMessage = "Nome inválido")]
        public string NameOpt { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório!")]
        public DateTime DtOpen { get; set; }

        public bool? Status { get; set; }

        public Address Address { get; set; }
    }
}
