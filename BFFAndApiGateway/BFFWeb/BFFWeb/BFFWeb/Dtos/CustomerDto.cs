using System;

namespace BFFWeb.Dtos
{
    [Serializable]
    public class CustomerDto
    {

        public CustomerDto()
        {

        }
        
        public Guid CustomerId { get; set; }

        public string Dni { get; set; }
    }
}
