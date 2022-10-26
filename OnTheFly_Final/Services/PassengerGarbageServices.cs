using MongoDB.Driver;
using OnTheFly_Final.Models;
using OnTheFly_Final.Utils;

namespace OnTheFly_Final.Services
{
    public class PassengerGarbageServices
    {
        private readonly IMongoCollection<PassengerGarbage> _passengerGarbageServices;

        public PassengerGarbageServices(IDataBaseSettings settings)
        {
            var passenger = new MongoClient(settings.ConnectionString);
            var database = passenger.GetDatabase(settings.PassengerDataBaseName);
            _passengerGarbageServices = database.GetCollection<PassengerGarbage>(settings.PassengerGarbageCollectionName);
        }

        public PassengerGarbage CreatePassengerGarbage(PassengerGarbage passengerGarbage)
        {
            _passengerGarbageServices.InsertOne(passengerGarbage);
            return passengerGarbage;
        }
        public PassengerGarbage GetPassengerGarbage(string cpf) => _passengerGarbageServices.Find<PassengerGarbage>(passengerGarbage => passengerGarbage.CPF == cpf).FirstOrDefault();

    }
}
