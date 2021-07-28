using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GroveAirlines.DatabaseLayer.Models;
using GroveAirlines.RepositoryLayer;
using GroveAirlines.Views;

namespace GroveAirlines.ServiceLayer
{
    public class AirportService
    {
        private readonly AirportRepository _airportRepository;

        public AirportService(AirportRepository airportRepository)
        {
            _airportRepository = airportRepository;
        }

        [MethodImpl(MethodImplOptions.NoInlining)] // parameter less constructor only for testing
        public AirportService()
        {
            if (Assembly.GetExecutingAssembly().FullName == Assembly.GetCallingAssembly().FullName)
            {
                throw new Exception("This constructor should only be used for testing");
            }
        }

        public virtual async IAsyncEnumerable<AirportView> GetAllAirports()
        {
            Queue<Airport> airports = _airportRepository.GetAllAirports();
            foreach (Airport airport in airports)
            {
                yield return new AirportView(airport.AirportId, airport.City, airport.Iata);
            }
        }
    }
}
