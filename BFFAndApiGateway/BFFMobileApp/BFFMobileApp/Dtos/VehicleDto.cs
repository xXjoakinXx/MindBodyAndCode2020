using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BFFMobileApp.Dtos
{
    [Serializable]
    public class VehicleDto
    {
        public VehicleDto()
        {

        }

        public Guid CustomerId { get; set; }

        public Guid VechicleId { get; set; }

        public string Enrollment { get; set; }

        public string Brand { get; set; }
    }
}
