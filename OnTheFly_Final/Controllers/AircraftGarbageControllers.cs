using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnTheFly_Final.Models;
using OnTheFly_Final.Services;
using System.Collections.Generic;

namespace OnTheFly_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftGarbageControllers : ControllerBase
    {
        private readonly AircraftGarbageServices _aircraftGarbageServices;
        private readonly AircraftServices _aircraftServices;
        public AircraftGarbageControllers(AircraftGarbageServices aircraftGarbageServices, AircraftServices aircraftServices)
        {
            _aircraftGarbageServices = aircraftGarbageServices;
            _aircraftServices = aircraftServices;
        }

        [HttpPost]
        public ActionResult<AircraftGarbage> PostAircraft(AircraftGarbage aircraftGarbage, string rab)
        {
            Aircraft aircraftIn = _aircraftServices.GetAircraft(rab);
            
            aircraftGarbage.RAB = aircraftIn.RAB;
            aircraftGarbage.Capacity = aircraftIn.Capacity;
            aircraftGarbage.DtRegistry = aircraftIn.DtRegistry;
            aircraftGarbage.DtLastFlight = aircraftIn.DtLastFlight;
            //aircraftGarbage.Company=aircraftIn.Company;

            _aircraftGarbageServices.CreateAircraftGarbage(aircraftGarbage);
            return CreatedAtRoute("GetAircraftGarbage", new { rab = aircraftIn.RAB.ToString() }, aircraftIn);
        }
        [HttpGet]
        public ActionResult<List<AircraftGarbage>> GetAllAircraft()=>_aircraftGarbageServices.GetAllAircraftGarbage();
        [HttpGet("{rab:length(6)}", Name = "GetAircraftGarbage")]
        public ActionResult<Aircraft> GetAircraftGarbage(string rab)
        {
            var plane = _aircraftGarbageServices.GetAircraftGarbage(rab);
            if (plane == null) return NotFound();
            return Ok(plane);
        }

    }
}
