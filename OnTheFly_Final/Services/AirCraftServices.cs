using MongoDB.Driver;
using OnTheFly_Final.Models;
using OnTheFly_Final.Utils;
using System;
using System.Collections.Generic;

namespace OnTheFly_Final.Services
{
    public class AircraftServices
    {
        private readonly IMongoCollection<AirCraft> _aircraft;

        public AircraftServices(IDataBaseSettings settings)
        {
            var aircraft = new MongoClient(settings.ConnectionString);
            var database = aircraft.GetDatabase(settings.AircraftDatabaseName);
            _aircraft = database.GetCollection<AirCraft>(settings.AircraftCollectionName);
        }

        public AirCraft CreateAircraft(AirCraft aircraft)
        {
            _aircraft.InsertOne(aircraft);
            return aircraft;
        }

        public AirCraft GetAircraft(string rab) => _aircraft.Find<AirCraft>(aircraft => aircraft.RAB == rab).FirstOrDefault();

        public List<AirCraft> GetAllAircraft() => _aircraft.Find<AirCraft>(aircraft => true).ToList();

        public void UpdateAircraft(string rab, AirCraft aircraftIn) => _aircraft.ReplaceOne(aircraft => aircraft.RAB == rab, aircraftIn);

        public void RemoveAircraft(AirCraft aircraftIn)
        {
            _aircraft.DeleteOne(aircraft => aircraft.RAB == aircraftIn.RAB);
        }
    }
}
