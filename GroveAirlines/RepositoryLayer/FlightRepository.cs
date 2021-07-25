using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GroveAirlines.DatabaseLayer;
using GroveAirlines.DatabaseLayer.Models;
using GroveAirlines.Exceptions;
using GroveAirlines.Views;
using Microsoft.EntityFrameworkCore;

namespace GroveAirlines.RepositoryLayer
{
    public class FlightRepository
    {
        private readonly GroveAirlinesContext _context;

        public FlightRepository(GroveAirlinesContext context)
        {
            this._context = context;
        }

        [MethodImpl(MethodImplOptions.NoInlining)] // parameter less constructor only for testing
        public FlightRepository()
        {
            if (Assembly.GetExecutingAssembly().FullName == Assembly.GetCallingAssembly().FullName)
            {
                throw new Exception("This constructor should only be used for testing");
            }
        }

        public virtual async Task<Flight> GetFlightByFlightNumber(int flightNumber)
        {
            if (flightNumber.IsPositiveInteger())
                return await _context.Flight.FirstOrDefaultAsync(f => f.FlightNumber == flightNumber) ??
                       throw new FlightNotFoundException();
            Console.WriteLine(
                $"Argument exception in GetFlightByFlightNumber! flightNumber = {flightNumber}");
            throw new FlightNotFoundException();

        }

        public virtual Queue<Flight> GetAllFlights()
        {
            Queue<Flight> flights = new Queue<Flight>(_context.Flight.Count());
            foreach (Flight flight in _context.Flight)
            {
                flights.Enqueue(flight);
            }

            return flights;
        }
    }
}
