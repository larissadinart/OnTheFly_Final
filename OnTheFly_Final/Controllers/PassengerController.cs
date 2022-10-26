using System.Collections.Generic;
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
        private readonly AddressServices _addressServices;
        private readonly PassengerGarbageServices _passengerGarbageServices;

        public PassengerController(PassengerServices passengerServices, PassengerGarbageServices passengerGarbageServices, AddressServices addressServices)
        {
            _passengerServices = passengerServices;
            _addressServices = addressServices;
            _passengerGarbageServices = passengerGarbageServices;
        }

        [HttpGet]
        public ActionResult<List<Passenger>> GetAllPassenger() => _passengerServices.GetAllPassengers();


        [HttpGet("{cpf}", Name = "GetCpf")]
        public ActionResult<Passenger> GetPassengerCpf(string cpf) 
        {
            var pass = _passengerServices.GetPassenger(cpf);
            if (pass == null)
            {
                return NotFound();
            }
            return Ok(pass);
        }

        [HttpPost]
        public ActionResult<Passenger> PostPassenger(Passenger passenger)
        {
            Address address = _addressServices.Create(passenger.Address);
            passenger.Address = address;

            _passengerServices.CreatePassenger(passenger);
            return CreatedAtRoute("GetCpf", new { CPF = passenger.CPF.ToString() }, passenger);
        }

        [HttpPut]
        public ActionResult<Passenger> PutPassenger(Passenger passengerIn, string cpf) //nao esta encontrando o objeto no banco
        {
            var pass = _passengerServices.GetPassenger(cpf);

            if (pass == null)
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

        [HttpDelete]
        public ActionResult DeletePassenger(string cpf)
        {
            var passenger = _passengerServices.GetPassenger(cpf);

            if (passenger == null)
            {
                return NotFound("Passageiro não encontrado!");
            }
            else
            {
                PassengerGarbage passengerGarbage = new();

                passengerGarbage.CPF = passenger.CPF;
                passengerGarbage.Name = passenger.Name;
                passengerGarbage.Gender = passenger.Gender;
                passengerGarbage.Phone = passenger.Phone;
                passengerGarbage.DtBirth = passenger.DtBirth;
                passengerGarbage.DtRegister = passenger.DtRegister;
                passengerGarbage.Status = passenger.Status;
                passengerGarbage.Address = passenger.Address;

                _passengerServices.RemovePassenger(passenger, cpf);
                _passengerGarbageServices.CreatePassengerGarbage(passengerGarbage);

            }
            return NoContent();
        }


    }

}

