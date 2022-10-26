using System;
using MongoDB.Bson.Serialization.Attributes;

namespace OnTheFly_Final.Models
{
    [BsonIgnoreExtraElements]
    public class Flights
    {
        public Airports Destiny { get; set; }
        public AirCraft Plane { get; set; }
        public int Sales { get; set; }
        public DateTime Departure { get; set; }
        public bool Status { get; set; }
    }
}
