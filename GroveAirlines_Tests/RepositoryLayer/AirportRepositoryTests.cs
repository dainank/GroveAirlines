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
    public class AirportRepositoryTests
    {
        private GroveAirlinesContext _context;
        private AirportRepository _repository;

        [TestInitialize]
        public async Task TestInitialize()
        {
            DbContextOptions<GroveAirlinesContext> dbContextOptions =   // in memory temp database
                new DbContextOptionsBuilder<GroveAirlinesContext>().UseInMemoryDatabase("Grove").Options;
            _context = new GroveAirlinesContext(dbContextOptions);

            _repository = new AirportRepository(_context);
            Assert.IsNotNull(_repository);
        }

        [TestMethod]
        public async Task GetAirportByID_Success()
        {
            Airport airport = await _repository.GetAirportByID(1);
            Assert.IsNotNull(airport);
        }

        [DataRow(0)]
        [DataRow(-1)]
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public async Task GetAirportByID_Failure_InvalidInput(int airportID)
        {
            await _repository.GetAirportByID(airportID);
        }
    }
}
