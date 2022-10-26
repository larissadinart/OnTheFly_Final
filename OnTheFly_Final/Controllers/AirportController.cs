using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OnTheFly_Final.Models;
using OnTheFly_Final.Services;

namespace OnTheFly_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly AirportServices _airportservice;


        public AirportController(AirportServices airportServices)
        {
            _airportservice = airportServices;
        }

        [HttpGet("{iata}")]
        public ActionResult<Airports> GetFlights(string iata)
        {
            iata = iata.ToUpper();
            var destiny = _airportservice.GetAirports(iata);

            if (destiny == null)
            {
                return NotFound();
            }
            return Ok(destiny);
        }
    }
}
