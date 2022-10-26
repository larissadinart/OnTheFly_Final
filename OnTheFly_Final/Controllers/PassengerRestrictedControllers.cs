using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OnTheFly_Final.Models;
using OnTheFly_Final.Services;

namespace OnTheFly_Final.Controllers
{
    public class PassengerRestrictedControllers : ControllerBase
    {

        private readonly PassengerRestrictedServices _passengerRestrictedServices;

        public PassengerRestrictedControllers(PassengerRestrictedServices passengerRestrictedServices)
        {
            _passengerRestrictedServices = passengerRestrictedServices;
        }

        [HttpGet]
        public ActionResult<List<PassengerRestricted>> GetAllPassengerRestricted() => _passengerRestrictedServices.GetAllPassengersRestricteds();


        [HttpGet("{cpf}", Name = "GetCpfRestricted")]
        public ActionResult<Passenger> GetPassengerCpf(string cpf)
        {
            var pass = _passengerRestrictedServices.GetPassengerRestricted(cpf);
            if (pass == null)
            {
                return NotFound();
            }
            return Ok(pass);
        }

        [HttpPost]
        public ActionResult<PassengerRestricted> PostPassengerRestricted(PassengerRestricted passengerRestricted)
        {
            _passengerRestrictedServices.CreatePassengerRestricted(passengerRestricted);
            return CreatedAtRoute("GetCpfRestricted", new { CPF = passengerRestricted.CPF.ToString() }, passengerRestricted);
        }
    }
}
