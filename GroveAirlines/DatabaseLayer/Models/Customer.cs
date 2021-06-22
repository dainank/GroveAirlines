using System.Collections.Generic;

namespace GroveAirlines.DatabaseLayer.Models
{
    public sealed partial class Customer
    {
        public Customer(string name)
        {
            Booking = new HashSet<Booking>();
            Name = name;
        }

        public int CustomerId { get; set; }
        public string Name { get; set; }

        public ICollection<Booking> Booking { get; set; }
    }
}
