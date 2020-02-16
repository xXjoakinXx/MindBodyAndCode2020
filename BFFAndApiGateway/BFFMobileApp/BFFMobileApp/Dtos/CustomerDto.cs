using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BFFMobileApp.Dtos
{
    [Serializable]
    public class CustomerDto
    {
        public CustomerDto()
        {
                
        }

        public Guid CustomerId { get; set; }

        public string Name { get; set; }
    }
}
