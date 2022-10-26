using System.Net.Http;
using System.Threading.Tasks;
using MongoDB.Driver;
using OnTheFly_Final.Models;
using OnTheFly_Final.Utils;

namespace OnTheFly_Final.Services
{
    public class AirportServices
    {

        private readonly IMongoCollection<Airports> _airport;


        public AirportServices(IDataBaseSettings settings)
        {
            var airports = new MongoClient(settings.ConnectionString);
            var database = airports.GetDatabase(settings.AirportDataBaseName);
            _airport = database.GetCollection<Airports>(settings.AirportCollectionName);
        }

        public Airports GetAirports(string destiny) => _airport.Find<Airports>(airport => airport.IATA == destiny).FirstOrDefault();
    }
}
