using System;
using MongoDB.Bson.Serialization.Attributes;

namespace OnTheFly_Final.Models
{
    [BsonIgnoreExtraElements]
    public class PassengerRestricted
    {
        public String CPF { get; set; }
    }
}
