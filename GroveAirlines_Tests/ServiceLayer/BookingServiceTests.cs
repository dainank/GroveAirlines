using GroveAirlines.DatabaseLayer;
using GroveAirlines.DatabaseLayer.Models;
using GroveAirlines.Exceptions;
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
    [TestClass]
    public class BookingServiceTests
    {
        private Mock<BookingRepository> _mockBookingRepository;
        private Mock<CustomerRepository> _mockCustomerRepository;
        private Mock<FlightRepository> _mockFlightRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockBookingRepository = new Mock<BookingRepository>();
            _mockCustomerRepository = new Mock<CustomerRepository>();
            _mockFlightRepository = new Mock<FlightRepository>();
        }

        [TestMethod]
        public async Task CreateBooking_Success()
        {   
            _mockBookingRepository.Setup(repository => repository.CreateBooking(0, 0)).Returns(Task.CompletedTask);  // setup return
            _mockCustomerRepository.Setup(repository => repository.GetCustomerByName("Benjamin Whelan")).Returns(Task.FromResult(new Customer("Benjamin Whelan")));
            _mockFlightRepository.Setup(repository => repository.GetFlightByFlightNumber(0)).Returns(Task.FromResult(new Flight()));

            var service = new BookingService(_mockBookingRepository.Object, _mockFlightRepository.Object, _mockCustomerRepository.Object);  // inject mock into service
            
            var (result, exception) = await service.CreateBooking("Benjamin Whelan", 0); // create booking

            Assert.IsTrue(result);
            Assert.IsNull(exception);
        }

        [TestMethod]
        public async Task CreateBooking_Success_CustomerNotInDatabase()
        {
            _mockBookingRepository.Setup(repository => repository.CreateBooking(0, 0)).Returns(Task.CompletedTask);
            _mockCustomerRepository.Setup(repository => repository.GetCustomerByName("Benjamin Whelan"))
                .Throws((new CustomerNotFoundException()));

            BookingService service = new BookingService(_mockBookingRepository.Object, _mockFlightRepository.Object,
                _mockCustomerRepository.Object);

            (bool result, Exception exception) = await service.CreateBooking("Benjamin Whelan", 0);

            Assert.IsFalse(result);
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(CustomerNotFoundException));
        }


        [TestMethod]
        [DataRow("", 0)]
        [DataRow(null, -1)]
        [DataRow("Benjamin Whelan", -1)]
        public async Task CreateBooking_Failure_InvalidInputParameters(string customerName, int flightNumber)
        {
            _mockBookingRepository.Setup(repository => repository.CreateBooking(0, 1)).Throws(new ArgumentException());  // logic paths
            _mockBookingRepository.Setup(repository => repository.CreateBooking(1, 2)).Throws(new CouldNotAddBookingToDatabaseException());

            _mockCustomerRepository.Setup(repository => repository.GetCustomerByName("Benjamin Whelan")).Returns(Task.FromResult(new Customer("Benjamin Whelan") { CustomerId = 0 }));
            _mockCustomerRepository.Setup(repository => repository.GetCustomerByName("Scott Whelan")).Returns(Task.FromResult(new Customer("Scott Whelan"){CustomerId = 1}));

            var service = new BookingService(_mockBookingRepository.Object, _mockFlightRepository.Object, _mockCustomerRepository.Object);

            var (result, exception) = await service.CreateBooking("Benjamin Whelan", 1); // call false

            Assert.IsFalse(result);
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(CouldNotAddBookingToDatabaseException));  // assert exception

            (result, exception) = await service.CreateBooking("Scott Whelan", 2);   // call false

            Assert.IsFalse(result);
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(CouldNotAddBookingToDatabaseException));  // assert exception
        }

        [TestMethod]
        public async Task CreateBooking_Failure_RepositoryException_ArgumentException()
        {
            _mockBookingRepository.Setup(repository => repository.
            CreateBooking(0, 1)).Throws(new ArgumentException());

            _mockCustomerRepository.Setup(repository => repository.
            GetCustomerByName("Benjamin Whelan")).Returns(Task.FromResult(new Customer("Benjamin Whelan") { CustomerId = 0 }));
                // above mocks creating repository layer content

            var service = new BookingService(_mockBookingRepository.Object, _mockFlightRepository.Object, _mockCustomerRepository.Object);
            var (result, exception) = await service.CreateBooking("Benjamin Whelan", 1);

            Assert.IsFalse(result);
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(CouldNotAddBookingToDatabaseException));
        }

        [TestMethod]
        public async Task CreateBooking_Failure_FlightNotInDatabase()
        {
            _mockFlightRepository.Setup(repository => repository.GetFlightByFlightNumber(-1))
                .Throws(new FlightNotFoundException()); // mimicking
            var bookingService = new BookingService(_mockBookingRepository.Object, _mockFlightRepository.Object, _mockCustomerRepository.Object);
            var (result, exception) = await bookingService.CreateBooking("Benjamin Whelan", 12);

            Assert.IsFalse(result);
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(CouldNotAddBookingToDatabaseException));
        }

        [TestMethod]
        public async Task CreateBooking_Failure_CustomerNotInDatabase_RepositoryFailure()
        {
            _mockBookingRepository.Setup(repository => repository.CreateBooking(0, 0))
                .Throws(new CouldNotAddBookingToDatabaseException());   // create standard booking (customer/flight not exising)

            _mockFlightRepository.Setup(repository => repository.GetFlightByFlightNumber(0))
                .ReturnsAsync(new Flight());    // create new existing flight (id = 0)

            _mockCustomerRepository.Setup(repository => repository.GetCustomerByName("Benjamin Whelan"))
                .Returns(Task.FromResult(new Customer("Benjamin Whelan"))); // get existing person

            var bookingService = new BookingService(_mockBookingRepository.Object, _mockFlightRepository.Object,
                _mockCustomerRepository.Object);
            var (result, exception) = await bookingService.CreateBooking("Benjamin Whelan", 12);

            Assert.IsFalse(result);
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(CouldNotAddBookingToDatabaseException));

        }
    }
}
