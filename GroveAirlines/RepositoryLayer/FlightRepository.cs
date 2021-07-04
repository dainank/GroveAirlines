using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroveAirlines.DatabaseLayer;
using GroveAirlines.DatabaseLayer.Models;
using GroveAirlines.Exceptions;
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


        public async Task<Flight> GetFlightByFlightNumber(int flightNumber, int originAirportId, int destinationAirportId)
        {
            if (!originAirportId.IsPositiveInteger() || !destinationAirportId.IsPositiveInteger())
            {
                Console.WriteLine(
                    $"Argument exception in GetFlightByFlightNumber! originAirportId = {originAirportId}, destinationAirportId = {destinationAirportId}");
                throw new ArgumentException("Invalid parameters provided.");
            }

            if (!flightNumber.IsPositiveInteger())
            {
                Console.WriteLine(
                    $"Argument exception in GetFlightByFlightNumber! flightNumber = {flightNumber}");
                throw new FlightNotFoundException();
            }

            return await _context.Flight.FirstOrDefaultAsync(f => f.FlightNumber == flightNumber) ??
                   throw new FlightNotFoundException();
        }
    }
}
