using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnTheFly_Final.Models
{
    [BsonIgnoreExtraElements]
    public class Company
    {
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [StringLength(18, ErrorMessage = "Número de CNPJ inválido")]
        public string CNPJ { get; set; }


        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [StringLength(30, ErrorMessage = "Nome inválido")]
        public string Name { get; set; }


        [StringLength(30, ErrorMessage = "Nome inválido")]
        public string NameOpt { get; set; }


        [Required(ErrorMessage = "Este campo é obrigatório!")]
        public DateTime DtOpen { get; set; }

        public bool ? Status { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório!")]
        public Address Address { get; set; }

        //[Required(ErrorMessage = "Este campo é obrigatório!")]
        //public List<Airplane> Airplanes { get; set; }
    }
}
