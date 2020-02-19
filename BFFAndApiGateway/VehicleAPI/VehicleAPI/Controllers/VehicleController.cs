using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using VehicleAPI.Model;

namespace VehicleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private static readonly Vehicle[] _vehicles = new Vehicle[]{
            new Vehicle() { VechicleId = Guid.NewGuid(), CustomerId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"), Brand = "Renault", Enrollment = "9724HFH" },
            new Vehicle() { VechicleId = Guid.NewGuid(), CustomerId = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7"), Brand = "Audi", Enrollment = "3425KJD" }
            };


        private readonly ILogger<VehicleController> _logger;

        public VehicleController(ILogger<VehicleController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Vehicle>> Get()
        {
            return Ok(_vehicles);
        }
    }
}
