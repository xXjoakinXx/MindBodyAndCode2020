using CustomerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CustomerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private static readonly Customer[] _customers = new Customer[]{ 
            new Customer() { CustomerId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"), Dni = "77846465R", Name = "Antonio"},
            new Customer() { CustomerId = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7"), Dni = "33472726T", Name = "Francisco"}
            };

        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return Ok(_customers);
        }
    }
}
