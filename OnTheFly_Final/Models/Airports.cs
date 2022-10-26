using MongoDB.Bson.Serialization.Attributes;

namespace OnTheFly_Final.Models
{
    [BsonIgnoreExtraElements]
    public class Airports
    {
        public string IATA { get; set; }
        public string State { get; set; }
        public string Coutry { get; set; }
    }
}
