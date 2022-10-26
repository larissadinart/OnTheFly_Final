using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OnTheFly_Final.Models;
using OnTheFly_Final.Services;

namespace OnTheFly_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly FlightsServices _flightsServices;
        private readonly AircraftServices _airCraftServices;
        private readonly AirportServices _airportServices;

        public FlightsController(FlightsServices flightServices, AircraftServices airCraftServices, AirportServices airportServices)
        {
            _flightsServices = flightServices;
            _airCraftServices = airCraftServices;
            _airportServices = airportServices;
        }

        [HttpGet]
        public ActionResult<List<Flights>> GetAllFlights() => _flightsServices.GetAllFlights();

        [HttpGet("{Date}", Name = "GetFlights")]
        public ActionResult<Flights> GetFlights(string iata, DateTime date)
        {
            var destiny = _airportServices.GetAirports(iata);
            if (destiny == null)
            {
                return NotFound();
            }
            else
            {
                var flight = _flightsServices.GetFlights(destiny.IATA, date);

                if (flight == null)
                {
                    return NotFound();
                }
                return Ok(flight);
            }
        }

        [HttpPost]
        public ActionResult<Flights> PostFlights(Flights flights)
        {

            var destiny = _airportServices.GetAirports(flights.Destiny.IATA);
            if (destiny == null)
            {
                return NotFound();
            }
            else
            {
                var plane = _airCraftServices.GetAircraft(flights.Plane.RAB);
                if (plane == null)
                {
                    return NotFound();
                }
                else
                {
                    _flightsServices.CreateFlights(flights);
                    return Ok(flights);
                }
            }

        }

        [HttpPut("{Destiny}/{Date}")]
        public Flights UpdatePassenger(string destiny, DateTime date, Flights flights)
        {
            var flight = _flightsServices.GetFlights(destiny, date);
            if (flight == null)
            {
                return null;
            }


            _flightsServices.UpdateFlights(flights, destiny, date);
            return flight;

        }
    }
}
