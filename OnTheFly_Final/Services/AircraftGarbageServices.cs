using MongoDB.Driver;
using OnTheFly_Final.Models;
using OnTheFly_Final.Utils;

namespace OnTheFly_Final.Services
{
    public class AircraftGarbageServices
    {
        private readonly IMongoCollection<AircraftGarbage> _aircraftGarbage;

        public AircraftGarbageServices(IDatabaseSettings settings)
        {
            var aircraft = new MongoClient(settings.ConnectionString);
            var database = aircraft.GetDatabase(settings.DatabaseAircraft);
            _aircraftGarbage = database.GetCollection<AircraftGarbage>(settings.AircraftCollectionName);
        }

        public AircraftGarbage CreateAircraftGarbage(AircraftGarbage aircraftgarbage)
        {
            _aircraftGarbage.InsertOne(aircraftgarbage);
            return aircraftgarbage;
        }

        public AircraftGarbage GetAircraftGarbage(string rab) => _aircraftGarbage.Find<AircraftGarbage>(aircraftGarbage => aircraftGarbage.RAB == rab).FirstOrDefault();
    }
}
