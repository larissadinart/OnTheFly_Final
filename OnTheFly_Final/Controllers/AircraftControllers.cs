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
        private readonly AircraftGarbageServices _aircraftGarbageServices;

        public AircraftControllers(AircraftServices aircraftServices, AircraftGarbageServices aircraftGarbageServices)
        {
            _aircraftServices = aircraftServices;
            _aircraftGarbageServices = aircraftGarbageServices;
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
        [HttpDelete]
        public ActionResult<Aircraft> DeleteAircraft(Aircraft aircraftIn)
        {
        
            Aircraft aircraft = _aircraftServices.GetAircraft(aircraftIn.RAB);
            if (aircraft == null) return NotFound("Aeronave não encontrada");

           
            AircraftGarbage aircraftGarbage = new AircraftGarbage();    //crio novo objeto

            //populo esse novo objeto
            aircraftGarbage.Id = aircraftIn.Id;
            aircraftGarbage.RAB = aircraftIn.RAB;
            aircraftGarbage.Capacity = aircraftIn.Capacity;
            aircraftGarbage.DtRegistry = aircraftIn.DtRegistry;
            aircraftGarbage.DtLastFlight = aircraftIn.DtLastFlight;
            //aircraftGarbage.Company=aircraftIn.Company;
            _aircraftServices.RemoveAircraft(aircraftIn);
            //insiro na coleção de "lixeira"
            _aircraftGarbageServices.CreateAircraftGarbage(aircraftGarbage);
           
           
            return NoContent();

        }
    }
}
