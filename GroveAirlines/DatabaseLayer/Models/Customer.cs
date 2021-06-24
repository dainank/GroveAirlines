using System;
using System.Collections.Generic;

namespace GroveAirlines.DatabaseLayer.Models
{
    public sealed class Customer
    {
        public int CustomerId { get; set; } // PK
        public string Name { get; set; }

        public ICollection<Booking> Booking { get; set; }   // List of bookings

        public Customer(string name)
        {
            Booking = new HashSet<Booking>();   // on constructor call create customer list
            Name = name;   
        }
    }
}
