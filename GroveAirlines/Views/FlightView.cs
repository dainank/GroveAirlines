using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroveAirlines.Views
{
    public class FlightView // has input validation
    {
        public string FlightNumber { get; } // private set auto
        public AirportInfo Origin { get; }
        public AirportInfo Destination { get; }

        public FlightView(string flightNumber, (string city, string code) origin,
            (string city, string code) destination)
        {
            FlightNumber = string.IsNullOrEmpty(flightNumber) ? "No flight number found." : flightNumber;
            Origin = new AirportInfo(origin);
            Destination = new AirportInfo(destination);
        }

        public struct AirportInfo
        {
            public string City { get; set; }
            public string Code { get; set; }

            public AirportInfo((string city, string code) airport)
            {
                City = string.IsNullOrEmpty(airport.city) ? "No city found." : airport.city;
                Code = string.IsNullOrEmpty(airport.code) ? "No airport code found." : airport.code;
            }
        }
    }
}
