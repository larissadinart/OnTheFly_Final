using System;
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
        public ActionResult<Passenger> PostPassenger(string cpf, string district,string name, char gender, string phone, DateTime dtBirth, string zip, string street, int number, string compl, string city, string state  )
        {
            var passenger = new Passenger
            {
                CPF = cpf.Substring(0, 3) + "." + cpf.Substring(3, 3) + "." + cpf.Substring(6, 3) + "-" + cpf.Substring(9, 2),
                Name = name,
                Gender = gender,
                Phone = "(" + phone.Substring(0, 2) + ")" + phone.Substring(2, 4) + "-" + phone.Substring(6, 4),
                DtBirth = dtBirth,
                Status = true,
                DtRegister = DateTime.Now,
                Address = new AddressServices().GetAddress(zip)

            };

            if (ValidarCpf(passenger.CPF) == false)
            {
                NotFound();
            }
            else if (passenger.CPF == null)
            {
                passenger.Address = new Address
                {
                    ZipCode = zip,
                    Street = street,
                    City = city,
                    Complement = compl,
                    Number = number,
                    District = district,
                    State = state
                };
            }
            else
            {
                passenger.Address.Complement = compl;
                passenger.Address.Number = number;
            }

            _addressServices.Create(passenger.Address);
            _passengerServices.CreatePassenger(passenger);
            return CreatedAtRoute("GetCpf", new { CPF = passenger.CPF.ToString() }, passenger);
        }

        [HttpPut]
        public ActionResult<Passenger> PutPassenger([FromQuery] string cpf, string name, char gender, string phone, bool status) //nao esta encontrando o objeto no banco
        {
            var pass = _passengerServices.GetPassenger(cpf);

            if (pass == null)
            {
                return NotFound();
            }
            else
            {
                Passenger passengerIn = new() { CPF = pass.CPF, Name = name, Gender = gender, Phone = phone, DtBirth = pass.DtBirth, DtRegister = pass.DtRegister, Address = pass.Address, Status = status};
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

        public bool ValidarCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }


    }

}

