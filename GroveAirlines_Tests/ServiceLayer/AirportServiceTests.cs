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
    public class AirportServiceTests
    {
        private Mock<AirportRepository> _mockAirportRepository;

        [TestInitialize]
        public void Initialize()
        {
            _mockAirportRepository = new Mock<AirportRepository>();

            _mockAirportRepository.Setup(repository => repository.GetAirportById(31)).ReturnsAsync(
                new GroveAirlines.DatabaseLayer.Models.Airport
                {
                    AirportId = 31,
                    City = "Amsterdam",
                    Iata = "AMS"
                });

            _mockAirportRepository.Setup(repository => repository.GetAirportById(92)).ReturnsAsync(
                new GroveAirlines.DatabaseLayer.Models.Airport
                {
                    AirportId = 92,
                    City = "Manchester",
                    Iata = "MAN"
                });

            Airport airportInDatabase = new Airport
            {
                AirportId = 3,
                City = "Amsterdam",
                Iata = "AMS",
            };

            Queue<Airport> mockReturn = new Queue<Airport>(1);
            mockReturn.Enqueue(airportInDatabase);

            _mockAirportRepository.Setup(r => r.GetAllAirports()).Returns(mockReturn);
            _mockAirportRepository.Setup(r => r.GetAirportById(3)).Returns(Task.FromResult(airportInDatabase));
        }

        [TestMethod]
        public async Task GetAirports_Success()
        {
            AirportService service = new AirportService(_mockAirportRepository.Object);

            await foreach (AirportView airportView in service.GetAllAirports())
            {
                Assert.IsNotNull(airportView);
                Assert.AreEqual(airportView.Iata, "AMS");
                Assert.AreEqual(airportView.City, "Amsterdam");
                Assert.AreEqual(airportView.AirportId, 3);
            }
        }

        //[TestMethod]
        //[ExpectedException(typeof(AirportNotFoundException))]
        //public async Task GetAirports_Failure_RepositoryException()
        //{
        //    _mockAirportRepository.Setup(repository => repository.GetAirportById(31))
        //        .ThrowsAsync(new FlightNotFoundException());

        //    AirportService service = new AirportService(_mockAirportRepository.Object);
        //    await foreach (AirportView _ in service.GetAllAirports())
        //    {
        //        ;
        //    }
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        //public async Task GetAirports_Failure_RegularException()
        //{
        //    _mockAirportRepository.Setup(repository => repository.GetAirportById(31))
        //        .ThrowsAsync(new NullReferenceException());

        //    AirportService service = new AirportService(_mockAirportRepository.Object);
        //    await foreach (AirportView _ in service.GetAllAirports())
        //    {
        //        ;
        //    }
        //}
    }
}
