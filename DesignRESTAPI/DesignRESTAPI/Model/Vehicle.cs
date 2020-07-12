using System;

namespace DesignRESTAPI.Model
{
    public class Vehicle
    {
        public Vehicle()
        {
            // This is not the best way to generate auto-IDs. Just to simplify the example
            ID = new Random().Next(int.MaxValue);
        }

        public int ID { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Marca { get; set; }
    }
}
