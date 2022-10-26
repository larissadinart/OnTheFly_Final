using System.Collections.Generic;
using MongoDB.Driver;
using OnTheFly_Final.Models;
using OnTheFly_Final.Utils;

namespace OnTheFly_Final.Services
{
    public class PassengerRestrictedServices
    {
        private readonly IMongoCollection<PassengerRestricted> _passengerRestrictedServices;

        public PassengerRestrictedServices(IDataBaseSettings settings)
        {
            var passengerRestricted = new MongoClient(settings.ConnectionString);
            var database = passengerRestricted.GetDatabase(settings.PassengerDataBaseName);
            _passengerRestrictedServices = database.GetCollection<PassengerRestricted>(settings.PassengerRestrictedCollectionName);
        }

        public PassengerRestricted CreatePassengerRestricted(PassengerRestricted passengerRestricted)
        {
            _passengerRestrictedServices.InsertOne(passengerRestricted);
            return passengerRestricted;
        }
        public List<PassengerRestricted> GetAllPassengersRestricteds() => _passengerRestrictedServices.Find(passengerRestrcited => true).ToList();
        public PassengerRestricted GetPassengerRestricted(string cpf) => _passengerRestrictedServices.Find<PassengerRestricted>(passengerRestrcited => passengerRestrcited.CPF == cpf).FirstOrDefault();
    }
}
