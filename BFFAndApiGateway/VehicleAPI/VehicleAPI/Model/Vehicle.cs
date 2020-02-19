using System;

namespace VehicleAPI.Model
{
    [Serializable]
    public class Vehicle
    {
        public Vehicle()
        {

        }

        public Guid VechicleId { get; set; }

        public Guid CustomerId { get; set; }

        public string Enrollment { get; set; }

        public string Brand { get; set; }
    }
}
