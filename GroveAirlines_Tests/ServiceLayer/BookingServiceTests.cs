using GroveAirlines.DatabaseLayer;
using GroveAirlines.DatabaseLayer.Models;
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
        [TestMethod]
        public async Task CreateBooking_Success()
        {
            Mock<BookingRepository> mockBookingRepository = new Mock<BookingRepository>();  // mock repository
            Mock<CustomerRepository> mockCustomerRepository = new Mock<CustomerRepository>();

            mockBookingRepository.Setup(repository => repository.CreateBooking(0, 0)).Returns(Task.CompletedTask);  // setup return
            mockCustomerRepository.Setup(repository => repository.GetCustomerByName("Benjamin Whelan")).Returns(Task.FromResult(new Customer("Benjamin Whelan")));

            BookingService service = new BookingService(mockBookingRepository.Object, mockCustomerRepository.Object);  // inject mock into service
            
            (bool result, Exception exception) = await service.CreateBooking("Benjamin Whelan", 0); // create booking

            Assert.IsTrue(result);
            Assert.IsNull(exception);
        }
    }
}
