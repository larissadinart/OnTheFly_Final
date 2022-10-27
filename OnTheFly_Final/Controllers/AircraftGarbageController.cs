using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnTheFly_Final.Models;
using OnTheFly_Final.Services;
using System.Collections.Generic;

namespace OnTheFly_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftGarbageController : ControllerBase
    {
        private readonly AircraftGarbageServices _aircraftGarbageServices;
        private readonly AircraftServices _aircraftServices;
        public AircraftGarbageController(AircraftGarbageServices aircraftGarbageServices, AircraftServices aircraftServices)
        {
            _aircraftGarbageServices = aircraftGarbageServices;
            _aircraftServices = aircraftServices;
        }

        [HttpPost]
        public ActionResult<AircraftGarbage> PostAircraft(string rab)
        {
            Aircraft aircraftIn = _aircraftServices.GetAircraft(rab);

            AircraftGarbage aircraftGarbage = new AircraftGarbage()
            {
                RAB = aircraftIn.RAB,
                Capacity = aircraftIn.Capacity,
                DtRegistry = aircraftIn.DtRegistry,
                DtLastFlight = aircraftIn.DtLastFlight,
                Company = aircraftIn.Company
            };

            _aircraftGarbageServices.CreateAircraftGarbage(aircraftGarbage);
            return CreatedAtRoute("GetAircraftGarbage", new { rab = aircraftIn.RAB.ToString() }, aircraftIn);
        }
        [HttpGet]
        public ActionResult<List<AircraftGarbage>> GetAllAircraft() => _aircraftGarbageServices.GetAllAircraftGarbage();
        [HttpGet("{rab:length(6)}", Name = "GetAircraftGarbage")]
        public ActionResult<Aircraft> GetAircraftGarbage(string rab)
        {
            var plane = _aircraftGarbageServices.GetAircraftGarbage(rab);
            if (plane == null) return NotFound();
            return Ok(plane);
        }

    }
}
