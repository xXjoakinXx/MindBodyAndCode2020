using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private static readonly Customer[] _customers = new Customer[]{ 
            new Customer() { CustomerId = Guid.NewGuid(), Dni = "77846465R", Name = "Antonio"},
            new Customer() { CustomerId = Guid.NewGuid(), Dni = "33472726T", Name = "Francisco"}
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
