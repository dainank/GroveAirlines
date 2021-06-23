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
    }
}
