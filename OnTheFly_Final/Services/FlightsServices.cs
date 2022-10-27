using System;
using System.Collections.Generic;
using MongoDB.Driver;
using OnTheFly_Final.Models;
using OnTheFly_Final.Utils;

namespace OnTheFly_Final.Services
{
    public class FlightsServices
    {
        private readonly IMongoCollection<Flights> _flight;

        public FlightsServices(IDataBaseSettings settings)
        {
            var flights = new MongoClient(settings.ConnectionString);
            var database = flights.GetDatabase(settings.FlightsDataBaseName);
            _flight = database.GetCollection<Flights>(settings.FlightCollectionName);
        }
        public Flights CreateFlights(Flights flights)
        {
            _flight.InsertOne(flights);
            return flights;
        }
        public List<Flights> GetAllFlights() => _flight.Find(flights => true).ToList();
        public Flights GetFlights(string iata, DateTime dateTime) => _flight.Find<Flights>(flights => flights.Departure == dateTime && flights.Destiny.IATA == iata).FirstOrDefault();

        public void UpdateFlights(Flights fligthsIn)
        {
            _flight.ReplaceOne(flights => flights.Plane == fligthsIn.Plane, fligthsIn);
        }
       
    }
}
