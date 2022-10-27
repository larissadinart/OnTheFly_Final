using MongoDB.Driver;
using OnTheFly_Final.Models;
using OnTheFly_Final.Utils;
using System;
using System.Collections.Generic;

namespace OnTheFly_Final.Services
{
    public class AircraftServices
    {
        private readonly IMongoCollection<Aircraft> _aircraft;

        public AircraftServices(IDataBaseSettings settings)
        {
            var aircraft = new MongoClient(settings.ConnectionString);
            var database = aircraft.GetDatabase(settings.AircraftDataBaseName);
            _aircraft = database.GetCollection<Aircraft>(settings.AircraftCollectionName);
        }

        public Aircraft CreateAircraft(Aircraft aircraft)
        {
            string test = aircraft.RAB;
            _aircraft.InsertOne(aircraft);
            return aircraft;
        }

        public Aircraft GetAircraft(string rab) => _aircraft.Find<Aircraft>(aircraft => aircraft.RAB == rab).FirstOrDefault();

        public List<Aircraft> GetAllAircraft() => _aircraft.Find<Aircraft>(aircraft => true).ToList();

        public void UpdateAircraft(string rab, Aircraft aircraft)
        {
            string test = aircraft.RAB;
            _aircraft.ReplaceOne(aircraft => aircraft.RAB == rab, aircraft);
        }

        public void RemoveAircraft(Aircraft aircraft)
        {
            string test = aircraft.RAB;
            _aircraft.DeleteOne(aircraft => aircraft.RAB == test);
        }
    }
}
