using BFFMobileApp.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BFFMobileApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private string CustomerAPIUrl = "http://localhost/";

        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<CustomerDto> Get()
        {
            //TODO: Http call to CustomerAPI service to get customers and return personal DTO
        }
    }
}
