using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroveAirlines.DatabaseLayer;

namespace GroveAirlines.RepositoryLayer
{
    public class BookingRepository
    {
        private readonly GroveAirlinesContext _context;

        public BookingRepository(GroveAirlinesContext context)
        {
            this._context = context;
        }

        public async Task CreateBooking(int customerId, int flightNumber)
        {
            if (customerId < 1 || flightNumber < 1) // can't be negative
            {
                Console.WriteLine($"Argument Exception in CreateBooking! Customer ID = {customerId}, Flight Number = {flightNumber}");  // for dev
                throw new ArgumentException("Invalid parameters provided!");
            }
        }
    }
}
