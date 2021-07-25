using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroveAirlines.DatabaseLayer.Models;
using GroveAirlines.Exceptions;
using GroveAirlines.RepositoryLayer;
using GroveAirlines.ServiceLayer;
using GroveAirlines.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GroveAirlines_Tests.ServiceLayer
{
    [TestClass]
    public class FlightServiceTests
    {
        private Mock<FlightRepository> _mockFlightRepository;
        private Mock<AirportRepository> _mockAirportRepository;

        [TestInitialize]
        public void Initialize()
        {
            _mockFlightRepository = new Mock<FlightRepository>();
            _mockAirportRepository = new Mock<AirportRepository>();

            _mockAirportRepository.Setup(repository => repository.GetAirportById(31)).ReturnsAsync(new GroveAirlines.DatabaseLayer.Models.Airport
            {
                AirportId = 31,
                City = "Amsterdam",
                Iata = "AMS"
            });

            _mockAirportRepository.Setup(repository => repository.GetAirportById(92)).ReturnsAsync(new GroveAirlines.DatabaseLayer.Models.Airport
            {
                AirportId = 92,
                City = "Manchester",
                Iata = "MAN"
            });

            Flight flightInDatabase = new Flight
            {
                FlightNumber = 148,
                Origin = 31,
                Destination = 92
            };

            Queue<Flight> mockReturn = new Queue<Flight>(1);
            mockReturn.Enqueue(flightInDatabase);

            _mockFlightRepository.Setup(repository => repository.GetAllFlights()).Returns(mockReturn);
            _mockFlightRepository.Setup(repository => repository.GetFlightByFlightNumber(148))
                .Returns(Task.FromResult(flightInDatabase));
        }

        [TestMethod]
        public async Task GetFlights_Success()
        {
            FlightService service = new FlightService(_mockFlightRepository.Object, _mockAirportRepository.Object);

            await foreach (FlightView flightView in service.GetFlights())
            {
                Assert.IsNotNull(flightView);
                Assert.AreEqual(flightView.FlightNumber, "148");
                Assert.AreEqual(flightView.Origin.City, "Amsterdam");
                Assert.AreEqual(flightView.Origin.Code, "AMS");
                Assert.AreEqual(flightView.Destination.City, "Manchester");
                Assert.AreEqual(flightView.Destination.Code, "MAN");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FlightNotFoundException))]
        public async Task GetFlights_Failure_RepositoryException()
        {
            _mockAirportRepository.Setup(repository => repository.GetAirportById(31)).ThrowsAsync(new FlightNotFoundException());

            FlightService service = new FlightService(_mockFlightRepository.Object, _mockAirportRepository.Object);
            await foreach (FlightView _ in service.GetFlights())
            {
                ;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetFlights_Failure_RegularException()
        {
            _mockAirportRepository.Setup(repository => repository.GetAirportById(31)).ThrowsAsync(new NullReferenceException());

            FlightService service = new FlightService(_mockFlightRepository.Object, _mockAirportRepository.Object);
            await foreach (FlightView _ in service.GetFlights())
            {
                ;
            }
        }

        [TestMethod]
        public async Task GetFlightByFlightNumber_Success()
        {
            FlightService service = new FlightService(_mockFlightRepository.Object, _mockAirportRepository.Object);
            FlightView flightView = await service.GetFlightByFlightNumber(148);

            Assert.IsNotNull(flightView);
            Assert.AreEqual(flightView.FlightNumber, "148");
            Assert.AreEqual(flightView.Origin.City, "Amsterdam");
            Assert.AreEqual(flightView.Origin.Code, "AMS");
            Assert.AreEqual(flightView.Destination.City, "Manchester");
            Assert.AreEqual(flightView.Destination.Code, "MAN");
        }

        [TestMethod]
        [ExpectedException(typeof(FlightNotFoundException))]
        public async Task GetFlightByFlightNumber_Failure_RepositoryException_FlightNotFoundException()
        {
            _mockFlightRepository.Setup(repository => repository.GetFlightByFlightNumber(-1)).Throws(new FlightNotFoundException());
            FlightService service = new FlightService(_mockFlightRepository.Object, _mockAirportRepository.Object);

            await service.GetFlightByFlightNumber(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetFlightByFlightNumber_Failure_RepositoryException_Exception()
        {
            _mockFlightRepository.Setup(repository => repository.GetFlightByFlightNumber(-1)).Throws(new OverflowException());
            FlightService service = new FlightService(_mockFlightRepository.Object, _mockAirportRepository.Object);

            await service.GetFlightByFlightNumber(-1);
        }
    }
}
