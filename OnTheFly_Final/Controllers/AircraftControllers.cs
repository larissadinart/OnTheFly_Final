﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnTheFly_Final.Models;
using OnTheFly_Final.Services;
using OnTheFly_Final.Utils;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

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
            aircraft.RAB = aircraft.RAB.ToLower();
            var rab = validation.RabValidation(aircraft.RAB);
            if (rab != aircraft.RAB)
                return BadRequest("Aeronave não está de acordo com as normas");
            aircraft.RAB = rab.Substring(0, 2) + "-" + rab.Substring(2, 3);
            var plane = _aircraftServices.GetAircraft(aircraft.RAB);
            if (plane != null) return NotFound("Aeronave já cadastrada!");

            _aircraftServices.CreateAircraft(aircraft);
            return CreatedAtRoute("GetAircraft", new { rab = aircraft.RAB.ToString() }, aircraft);
        }



        [HttpGet("{rab:length(6)}", Name = "GetAircraft")]
        public ActionResult<Aircraft> GetAircraft(string rab)
        {
            rab = rab.ToLower();
            //rab = rab.Substring(0, 2) + "-" + rab.Substring(2, 3);
            var plane = _aircraftServices.GetAircraft(rab);
            if (plane == null) return NotFound();
            return Ok(plane);
        }

        [HttpGet]
        public ActionResult<List<Aircraft>> GetAllAircraft() => _aircraftServices.GetAllAircraft();

        [HttpPut]
        public ActionResult<Aircraft> PutAircraft(Aircraft aircraftIn, string rab)
        {
            aircraftIn.RAB = aircraftIn.RAB.ToLower();
            var registration = validation.RabValidation(aircraftIn.RAB);
            var aircraft = _aircraftServices.GetAircraft(rab);
            if (aircraft == null) return NotFound("Não encontrado!");
            aircraftIn.RAB = rab;
            _aircraftServices.UpdateAircraft(aircraft.RAB, aircraftIn);
            return NoContent();
        }
        [HttpDelete]
        public ActionResult<Aircraft> DeleteAircraft(Aircraft aircraftIn, string rab)
        {
            aircraftIn.RAB = aircraftIn.RAB.ToLower();
            var aircraft = _aircraftServices.GetAircraft(rab);
            if (aircraft == null) return NotFound("Aeronave não encontrada");


            AircraftGarbage aircraftGarbage = new AircraftGarbage();    //crio novo objeto

            //populo esse novo objeto

            aircraftGarbage.RAB = aircraft.RAB;
            aircraftGarbage.Capacity = aircraft.Capacity;
            aircraftGarbage.DtRegistry = aircraft.DtRegistry;
            aircraftGarbage.DtLastFlight = aircraft.DtLastFlight;
            //aircraftGarbage.Company=aircraft.Company;
            _aircraftServices.RemoveAircraft(aircraft);
            _aircraftGarbageServices.CreateAircraftGarbage(aircraftGarbage);

            //insiro na coleção de "lixeira"

            return NoContent();

        }
    }
}
