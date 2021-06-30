using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroveAirlines.DatabaseLayer;
using GroveAirlines.DatabaseLayer.Models;
using GroveAirlines.Exceptions;
using GroveAirlines.RepositoryLayer;
using GroveAirlines_Tests.Stubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GroveAirlines_Tests.RepositoryLayer
{
    [TestClass]
    public class BookingRepositoryTests
    {
        private GroveAirlinesContext _context;
        private BookingRepository _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            DbContextOptions<GroveAirlinesContext> dbContextOptions =   // in memory temp database
                new DbContextOptionsBuilder<GroveAirlinesContext>().UseInMemoryDatabase("Grove").Options;
            _context = new GroveAirlinesContext_Stub(dbContextOptions);

            _repository = new BookingRepository(_context);
            Assert.IsNotNull(_repository);
        }

        [TestMethod]
        public async Task CreateBooking_Success()
        {
            await _repository.CreateBooking(1, 0);
            Booking booking = _context.Booking.First();
            
            Assert.IsNotNull(booking);
            Assert.AreEqual(1, booking.CustomerId);
            Assert.AreEqual(0, booking.FlightNumber);
        }

        [TestMethod]
        [DataRow(-1, 0)]
        [DataRow(0, -1)]
        [DataRow(-1, -1)]
        [ExpectedException(typeof(ArgumentException))]
        public async Task CreateBooking_Failure_InvalidInputs(int customerId, int flightNumber)
        {
            await _repository.CreateBooking(customerId, flightNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(CouldNotAddBookingToDatabaseException))]
        public async Task CreateBooking_Failure_DatabaseError()
        {
            await _repository.CreateBooking(0, 1);
        }
    }
}
