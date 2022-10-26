using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnTheFly_Final.Models
{
    [BsonIgnoreExtraElements]
    public class Address
    {
        [Required(ErrorMessage = "Campo Zipcode é obrigatório!"),StringLength(9, ErrorMessage = "Campo Zipcode inválido!")]
        public String ZipCode { get; set; }
        [Required(ErrorMessage = "Campo Street é obrigatório!"), StringLength(100, ErrorMessage = "Campo Street inválido!")]
        public String Street { get; set; }
        [Required(ErrorMessage = "Campo Number é obrigatório!")]
        public int Number { get; set; }
        [StringLength(10),Required(ErrorMessage = "Campo Complement inválido!")]
        public String Complement { get; set; }
        [StringLength(30),Required(ErrorMessage = "Campo City inválido!")]
        public String City { get; set; }
        [StringLength(2),Required(ErrorMessage = "Campo State inválido!")]
        public String State { get; set; }
    }
}
