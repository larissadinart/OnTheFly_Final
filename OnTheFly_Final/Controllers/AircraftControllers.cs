using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnTheFly_Final.Models;
using OnTheFly_Final.Services;
using OnTheFly_Final.Utils;
using System.Collections.Generic;

namespace OnTheFly_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftControllers : ControllerBase
    {
        ValidationAircraft validation = new ValidationAircraft();
        private readonly AircraftServices _aircraftServices;

        public AircraftControllers(AircraftServices aircraftServices)
        {
            _aircraftServices = aircraftServices;
        }

        [HttpPost]
        public ActionResult<Aircraft> PostAircraft(Aircraft aircraft)
        {
            var rab = validation.RabValidation(aircraft.RAB);
            var plane=_aircraftServices.GetAircraft(rab);
            if (plane != null) return NotFound("Aeronave já cadastrada!");
            _aircraftServices.CreateAircraft(aircraft);
            return CreatedAtRoute("GetAircraft", new { rab = aircraft.RAB.ToString() }, aircraft);
        }

        [HttpGet("{rab:length(5)}", Name = "GetAircraft")]
        public ActionResult<Aircraft> GetAircraft(string rab)
        {
            var plane = _aircraftServices.GetAircraft(rab);
            if (plane == null) return NotFound();
            return Ok(plane);
        }

        [HttpGet]
        public ActionResult<List<Aircraft>> GetAllAircraft() => _aircraftServices.GetAllAircraft();

        [HttpPut]
        public ActionResult<Aircraft> PutAircraft(Aircraft aircraftIn, string rab)
        {
            var aircraft = _aircraftServices.GetAircraft(rab);
            if (aircraft == null) return NotFound("Não encontrado!");
            aircraftIn.RAB = rab;
            _aircraftServices.UpdateAircraft(aircraft.RAB, aircraftIn);
            return NoContent();
        }

    }
}
