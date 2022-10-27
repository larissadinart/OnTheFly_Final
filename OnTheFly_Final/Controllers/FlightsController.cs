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




        [HttpPost]
        public ActionResult<Flights> PostFlights(string iata, DateTime date, string rab, double hours, double minutes)
        {
            date = date.AddHours(hours - 3).AddMinutes(minutes);
            iata = iata.ToUpper();
            if (date < DateTime.Now)
            {
                return NotFound("Impossivel criar voo com data retroativa!");
            }
            else
            {

                var destiny = _airportServices.GetAirports(iata);
                if (destiny == null)
                {
                    return NotFound("Destino nao encontrado!");
                }
                else
                {


                    var plane = _airCraftServices.GetAircraft(rab);

                    if (plane == null)
                    {
                        return NotFound("Impossivel encontar Aeronave!");
                    }
                    else
                    {
                        //var restited = _airCraftServices.GetAircraftRestrited(plane.Company)
                        //if (restited == true )
                        //{
                        //    return NotFound("Infelizmente essa Compania não pode cadastrar voos");
                        //}
                        //else
                        //{

                        Flights flights = new Flights() { Status = true, Plane = plane, Destiny = destiny, Departure = date };

                        _flightsServices.CreateFlights(flights);
                        return Ok(flights);
                        //}
                    }

                }
            }

        }

        [HttpGet("{date}", Name = "GetFlights")]
        public ActionResult<Flights> GetFlights(string iata, DateTime date, double hours, double minutes)
        {
            date = date.AddHours(hours - 3).AddMinutes(minutes);
            iata = iata.ToUpper();
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

        [HttpPut("{date}")]
        public ActionResult<Flights> UpdateFlights(string iata, DateTime date, double hours, double minutes, bool status)
        {
            date = date.AddHours(hours).AddMinutes(minutes);
            iata = iata.ToUpper();
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
                else
                {
                flight.Status = status;
                _flightsServices.UpdateFlights(flight);
                return flight;

                }
            }


        }
    }
}
