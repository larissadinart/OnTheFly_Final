﻿using Microsoft.AspNetCore.Mvc;
using OnTheFly_Final.Models;
using OnTheFly_Final.Services;

namespace OnTheFly_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AddressController : ControllerBase
    {
        private readonly AddressServices _addressServices;

        public AddressController(AddressServices addressServices)
        {
            _addressServices = addressServices;
        }

        [HttpGet("{zipCode}")]
        public ActionResult<string> GetAddress(string zipCode)
        {
            var address = _addressServices.GetAddress(zipCode);

            if (address == null) return NotFound();

            return Ok(address);
        }
    }
}
