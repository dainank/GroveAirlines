using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroveAirlines.DatabaseLayer;
using GroveAirlines.DatabaseLayer.Models;
using GroveAirlines.RepositoryLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GroveAirlines_Tests.RepositoryLayer
{
    [TestClass]
    class BookingRepositoryTests
    {
        private GroveAirlinesContext _context;
        private BookingRepository _repository;

        [TestInitialize]
        public async Task TestInitialize()
        {
            DbContextOptions<GroveAirlinesContext> dbContextOptions =   // in memory temp database
                new DbContextOptionsBuilder<GroveAirlinesContext>().UseInMemoryDatabase("Grove").Options;
            _context = new GroveAirlinesContext(dbContextOptions);

            // Booking testBooking = new Booking();
            // _context.Booking.Add(testBooking);
            // await _context.SaveChangesAsync();

            _repository = new BookingRepository(_context);
            Assert.IsNotNull(_repository);
        }

        [TestMethod]
        public void CreateBooking_Success()
        {

        }
    }
}
