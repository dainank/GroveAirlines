using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroveAirlines.DatabaseLayer;
using GroveAirlines.DatabaseLayer.Models;

namespace GroveAirlines.RepositoryLayer
{
    public class AirportRepository
    {
        private readonly GroveAirlinesContext _context;

        public AirportRepository(GroveAirlinesContext context)
        {
            this._context = context;
        }

        public async Task<Airport> GetAirportByID(int airportID)
        {
            // validate
            if (airportID < 1)
            {
                Console.WriteLine($"Argument exception in GetAirportByID! airportID = {airportID}");    // retrieve airport
                throw new ArgumentException("Invalid parameters provided; please check parameters.");   // custom exception
            }
            return new Airport();   // return instance
        }
    }
}
