using DesignRESTAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace DesignRESTAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private static readonly IList<Vehicle> Vehicles = new List<Vehicle>
        {
            new Vehicle(){
                ID = 4,
                Color = "White",
                Marca = "Wolkswagen",
                Model = "Polo"
            },
            new Vehicle(){
                ID = 8,
                Color = "Blue",
                Marca = "Seat",
                Model = "Ibiza"
            },
            new Vehicle(){
                ID = 19,
                Color = "Red",
                Marca = "Toyota",
                Model = "Corolla"
            }
        };

        private readonly ILogger<VehicleController> _logger;

        public VehicleController(ILogger<VehicleController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Vehicle>> Get([FromQuery]string color)
        {
            var vehicle = Vehicles.Where(v => v.Color == color);
            return Ok(vehicle);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Vehicle>> Get(int id)
        {
            var vehicle = Vehicles.FirstOrDefault(v => v.ID == id);
            return Ok(vehicle);
        }

        [HttpPost]
        public ActionResult<Vehicle> Create(VehicleDto createVehicleDto)
        {
            var vehicle = new Vehicle()
            {
                Color = createVehicleDto.Color,
                Marca = createVehicleDto.Marca,
                Model = createVehicleDto.Model
            };

            Vehicles.Add(vehicle);

            return Ok(vehicle);
        } 

        [HttpPut("{id}")]
        public ActionResult Upsert(int id, [FromBody]VehicleDto vehicleDto)
        {
            var existingVehicle = Vehicles.FirstOrDefault(v => v.ID == id);

            if(existingVehicle is null)
            {
                var vehicle = new Vehicle()
                {
                    Color = vehicleDto.Color,
                    Marca = vehicleDto.Marca,
                    Model = vehicleDto.Model
                };

                Vehicles.Add(vehicle);
            }
            else
            {
                existingVehicle.Color = vehicleDto.Color;
                existingVehicle.Marca = vehicleDto.Marca;
                existingVehicle.Model = vehicleDto.Model;
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Remove(int id)
        {
            var vehicle = Vehicles.FirstOrDefault(v => v.ID == id);

            Vehicles.Remove(vehicle);

            return Ok();
        }
    }
}
