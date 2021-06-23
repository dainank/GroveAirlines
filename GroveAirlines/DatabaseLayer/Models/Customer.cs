using System;
using System.Collections.Generic;

namespace GroveAirlines.DatabaseLayer.Models
{
    public sealed class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }

        public ICollection<Booking> Booking { get; set; }

        public Customer(string name)
        {
            Booking = new HashSet<Booking>();
            Name = name;
        }
    }
}
