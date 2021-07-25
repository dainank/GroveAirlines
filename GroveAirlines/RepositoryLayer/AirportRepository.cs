using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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

        public AirportRepository(GroveAirlinesContext context)
        {
            this._context = context;
        }

        [MethodImpl(MethodImplOptions.NoInlining)] // parameter less constructor only for testing
        public AirportRepository()
        {
            if (Assembly.GetExecutingAssembly().FullName == Assembly.GetCallingAssembly().FullName)
            {
                throw new Exception("This constructor should only be used for testing");
            }
        }

        public virtual async Task<Airport> GetAirportById(int airportId)
        {
            // validate
            if (airportId.IsPositiveInteger())
                return await _context.Airport.FirstOrDefaultAsync(a => a.AirportId == airportId) ??
                       throw new AirportNotFoundException();
            Console.WriteLine($"Argument exception in GetAirportByID! airportID = {airportId}");    // retrieve airport
            throw new ArgumentException("Invalid parameters provided; please check parameters.");   // custom exception
            // wait for completion; retrieve first match; return first matching ID element; throw exception if false
        }
    }
}
