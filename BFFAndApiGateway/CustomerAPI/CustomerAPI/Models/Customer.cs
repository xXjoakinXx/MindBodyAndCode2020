using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string Dni { get; set; }
        public string Name { get; set; }
    }
}
