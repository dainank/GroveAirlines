using GroveAirlines.DatabaseLayer;
using GroveAirlines.RepositoryLayer;
using GroveAirlines.ServiceLayer;
using GroveAirlines_Tests.Stubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroveAirlines_Tests.ServiceLayer
{
    public class BookingServiceTests
    {
        private GroveAirlinesContext _context;

        [TestInitialize]
        public void TestInitialize()
        {
            DbContextOptions<GroveAirlinesContext> dbContextOptions =   // in memory temp database
               new DbContextOptionsBuilder<GroveAirlinesContext>().UseInMemoryDatabase("Grove").Options;
            _context = new GroveAirlinesContext_Stub(dbContextOptions);
        }

        [TestMethod]
        public async Task CreateBooking_Success()
        {
            Mock<BookingRepository> mockRepository = new Mock<BookingRepository>();
            mockRepository.Setup(repository =>
            repository.CreateBooking(0, 0)).Returns(Task.CompletedTask);

            BookingService service = new BookingService(mockRepository);    // inject repo
            (bool result, Exception exception) = await service.CreateBooking("Benjamin Whelan", 0);
        }
    }
}
