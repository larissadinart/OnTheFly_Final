using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnTheFly_Final.Models
{
    public class Aircraft
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório!"), StringLength(5, ErrorMessage = "RAB inválido!")]
        public string RAB { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        public int Capacity { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        public DateTime DtRegistry { get; set; }
        public DateTime DtLastFlight { get; set; }
        //  [Required(ErrorMessage = "Este campo é obrigatório!"), StringLength(19, ErrorMessage = "CNPJ inválido!")]
        // public Company Company{ get; set; }
        public Aircraft()
        {
            DtRegistry=DateTime.Now;
            this.DtLastFlight = DtRegistry;
        }
    }
}
