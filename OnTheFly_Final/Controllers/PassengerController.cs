using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;
using Microsoft.AspNetCore.Mvc;
using OnTheFly_Final.Models;
using OnTheFly_Final.Services;

namespace OnTheFly_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly PassengerServices _passengerServices;

        public PassengerController(PassengerServices passengerServices)
        {
            _passengerServices = passengerServices;
        }

        [HttpGet]
        public ActionResult<List<Passenger>> GetAllPassenger() => _passengerServices.GetAllPassenger(); //criar metodo get

        [HttpGet("{Cpf:length(14)}", Name = "GetPassenger")]
        public ActionResult<Passenger> GetPassenger(string cpf)
        {
            var passenger = _passengerServices.GetPassenger(cpf);
            if(passenger == null)
            {
                return NotFound();
            }
            return Ok(passenger);
        }

        [HttpPost]
        public ActionResult<Passenger> PostPassenger([FromBody] Passenger passenger)
        {
            _passengerServices.CreatePassenger(passenger);
            return CreatedAtRoute("GetPassenger", new { CPF = passenger.CPF.ToString() }, passenger);
        }

        [HttpPut("{cpf}")]
        public ActionResult<Passenger> UpdatePassenger(Passenger passengerIn, string cpf)
        {
            var pass = _passengerServices.GetPassenger(cpf);
            if(pass == null)
            {
                return NotFound();
            }
            else
            {
                _passengerServices.UpdatePassenger(passengerIn, cpf);
                    pass = _passengerServices.GetPassenger(cpf);
                return Ok(pass);
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(string cpf)
        {
            var passenger = _passengerServices.GetPassenger(cpf);
            if(passenger != null)
            {
                _passengerServices.Remove(passenger, cpf);
                return NoContent();
            }return NotFound();
        }
    }
}
