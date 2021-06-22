using System.Collections.Generic;

namespace GroveAirlines.DatabaseLayer.Models
{
    public sealed partial class Flight
    {
        public Flight()
        {
            Booking = new HashSet<Booking>();
        }
        
        public int FlightNumber { get; set; }
        public int Origin { get; set; }
        public int Destination { get; set; }

        public Airport DestinationNavigation { get; set; }  // connects to FlightDestinationNavigation
        public Airport OriginNavigation { get; set; }   // connects to FlightOriginNavigation
        public ICollection<Booking> Booking { get; set; }
    }
}
