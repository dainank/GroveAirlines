using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroveAirlines.Views
{
    public class AirportView
    {
        public int AirportId { get; set; }
        public string City { get; set; }
        public string Iata { get; set; }

        public AirportView(int airportId, string city, string iata)
        {
            AirportId = airportId;
            City = city;
            Iata = iata;
        }
    }
}
