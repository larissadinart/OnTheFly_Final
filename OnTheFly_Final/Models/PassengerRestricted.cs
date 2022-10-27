using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace OnTheFly_Final.Models
{
    [BsonIgnoreExtraElements]
    public class PassengerRestricted
    {
        [Required]
        public String CPF { get; set; }
    }
}
