using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnTheFly_Final.Models
{
    public class Company
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório!")]
            [StringLength(19)]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [StringLength(19)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [StringLength(19)]
        public string NameOpt { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [StringLength(19)]
        public DateTime DtOpen { get; set; }
        public bool ? Status { get; set; }

        //[Required(ErrorMessage = "Este campo é obrigatório!")]
        //public Address Address { get; set; }
        //[Required(ErrorMessage = "Este campo é obrigatório!")]
        //public List<Airplane> Airplanes { get; set; }
    }
}
