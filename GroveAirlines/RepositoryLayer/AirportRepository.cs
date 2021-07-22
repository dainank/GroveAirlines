using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GroveAirlines.DatabaseLayer;
using GroveAirlines.DatabaseLayer.Models;
using GroveAirlines.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GroveAirlines.RepositoryLayer
{
    public class AirportRepository
    {
        private readonly GroveAirlinesContext _context;

        public AirportRepository(GroveAirlinesContext _context)
        {
            this._context = _context;
        }

        public async Task<Airport> GetAirportByID(int airportID)
        {
            // validate
            if (!airportID.IsPositiveInteger())
            {
                Console.WriteLine($"Argument exception in GetAirportByID! airportID = {airportID}");    // retrieve airport
                throw new ArgumentException("Invalid parameters provided; please check parameters.");   // custom exception
            }
            return await _context.Airport.FirstOrDefaultAsync(a => a.AirportId == airportID) ?? throw new AirportNotFoundException();
            // wait for completion; retrieve first match; return first matching ID element; throw exception if false
        }
    }
}
