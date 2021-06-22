using System.Collections.Generic;

namespace GroveAirlines.DatabaseLayer.Models
{
    public sealed partial class Airport
    {
        public Airport()
        {
            FlightDestinationNavigation = new HashSet<Flight>();
            FlightOriginNavigation = new HashSet<Flight>();
        }

        public int AirportId { get; set; }
        public string City { get; set; }
        public string Iata { get; set; }

        public ICollection<Flight> FlightDestinationNavigation { get; set; }    // picks up DestinationNavigation
        public ICollection<Flight> FlightOriginNavigation { get; set; } // picks up OriginNavigation
    }
}
