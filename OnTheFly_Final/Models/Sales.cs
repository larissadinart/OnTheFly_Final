using System.Collections.Generic;

namespace OnTheFly_Final.Models
{
    public class Sales
    {
        public Flights Flight { get; set; }
        public List<Passenger> Passagers { get; set; }
        public bool Reserved { get; set; }
        public bool Sold { get; set; }  
    }
}
