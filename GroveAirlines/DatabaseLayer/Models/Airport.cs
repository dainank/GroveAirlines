using System;
using System.Collections.Generic;

namespace GroveAirlines.DatabaseLayer.Models
{
    public sealed class Airport
    {
        public int AirportId { get; set; }  // PK
        public string City { get; set; }    
        public string Iata { get; set; }    // IATA airport code    

        public ICollection<Flight> FlightDestinationNavigation { get; set; }    // picks up DestinationNavigation
        public ICollection<Flight> FlightOriginNavigation { get; set; } // picks up OriginNavigation

        public Airport()
        {
            FlightDestinationNavigation = new HashSet<Flight>();
            FlightOriginNavigation = new HashSet<Flight>();
        }
    }
}
