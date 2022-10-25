using System.Collections.Generic;
using MongoDB.Driver;
using OnTheFly_Final.Models;
using OnTheFly_Final.Utils;

namespace OnTheFly_Final.Services
{
    public class PassengerServices
    {
        private readonly IMongoCollection<Passenger> _passenger;

        public PassengerServices(IDataBaseSettings settings)
        {
            var passenger = new MongoClient(settings.ConnectionString);
            var database = passenger.GetDatabase(settings.DataBaseName);
            _passenger = database.GetCollection<Passenger>(settings.PassengerCollectionName);
        }
        public Passenger CreatePassenger(Passenger passenger)
        {
            _passenger.InsertOne(passenger);
            return passenger;
        }
        public List<Passenger> GetAllPassenger() => _passenger.Find(passenger => true).ToList();
        public Passenger GetPassenger(string cpf) => _passenger.Find<Passenger>(passenger => passenger.CPF == cpf).FirstOrDefault();

        public void UpdatePassenger(Passenger passengerIn, string cpf)
        {
            _passenger.ReplaceOne(passenger => passenger.CPF == cpf, passengerIn);
        }
        public void Remove(Passenger passenger, string cpf) => _passenger.DeleteOne(passenger => passenger.CPF == cpf);


    }
}
