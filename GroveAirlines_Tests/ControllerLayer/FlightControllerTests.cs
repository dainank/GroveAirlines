using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GroveAirlines.ControllerLayer;
using GroveAirlines.Exceptions;
using GroveAirlines.ServiceLayer;
using GroveAirlines.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GroveAirlines_Tests.ControllerLayer
{
    [TestClass]
    public class FlightControllerTests
    {
        [TestMethod]
        public async Task GetAllFlights_Success()
        {
            Mock<FlightService> service = new Mock<FlightService>();    // instantiate

            List<FlightView> returnFlightViews = new List<FlightView>(2)    // create body
            {
                new FlightView("638", ("Amsterdam", "AMS"),
                    ("Manchester", "MAN")),
                new FlightView("938", ("Luxembourg", "LUX"),
                    ("Brussels", "BRU"))
            };
                // mock returns list of FlightViews
            service.Setup(s => s.GetFlights()).Returns(FlightViewAsyncGenerator(returnFlightViews));

            FlightController controller = new FlightController(service.Object);
            ObjectResult response = await controller.GetAllFlights() as ObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual((int) HttpStatusCode.OK, response.StatusCode);

            Queue<FlightView> content = response.Value as Queue<FlightView>;
            Assert.IsNotNull(content);

            Assert.IsTrue(returnFlightViews.All(flight => content.Contains(flight)));
        }

        private async IAsyncEnumerable<FlightView> FlightViewAsyncGenerator(IEnumerable<FlightView> views)
        {
            foreach (FlightView flightView in views)
            {
                yield return flightView;
            }
        }

        [TestMethod] // 404
        public async Task GetAllFlights_Failure_FlightNotFoundException_404()
        {
            Mock<FlightService> service = new Mock<FlightService>();    // no flight views
            service.Setup(s => s.GetFlights()).Throws(new FlightNotFoundException());

            FlightController controller = new FlightController(service.Object);
            ObjectResult response = await controller.GetAllFlights() as ObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual((int) HttpStatusCode.NotFound, response.StatusCode);
            Assert.AreEqual("No flights were found in the database.", response.Value);
        }

        [TestMethod] // 500
        public async Task GetAllFlights_Failure_InternalServerError_400()
        {
            Mock<FlightService> service = new Mock<FlightService>();
            service.Setup(s => s.GetFlights()).Throws(new ArgumentException());

            FlightController controller = new FlightController(service.Object);
            ObjectResult response = await controller.GetAllFlights() as ObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual((int) HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("Bad request.", response.Value);
        }

        [TestMethod]
        public async Task GetFlightByFlightNumber_Success()
        {
            Mock<FlightService> service = new Mock<FlightService>();

            FlightView returnedFlightView = new FlightView("0", ("Amsterdam", "AMS"),
                ("Manchester", "MAN"));
            service.Setup(s => s.GetFlightByFlightNumber(0)).Returns(Task.FromResult(returnedFlightView));

            FlightController controller = new FlightController(service.Object);

            ObjectResult response = await controller.GetFlightByFlightNumber(0) as ObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual((int) HttpStatusCode.OK, response.StatusCode);

            FlightView content = response.Value as FlightView;
            Assert.IsNotNull(content);

            await controller.GetFlightByFlightNumber(0);
        }

        [TestMethod]
        public async Task GetFlightByFlightNumber_Failure_FlightNotFoundException_404()
        {
            Mock<FlightService> service = new Mock<FlightService>();
            service.Setup(s => s.GetFlightByFlightNumber(1))
                .Throws(new FlightNotFoundException());

            FlightController controller = new FlightController(service.Object);
            ObjectResult response = await controller.GetFlightByFlightNumber(1) as ObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
            Assert.AreEqual("The flight was not found in the database.",
                response.Value);
        }

        [TestMethod]
        public async Task GetFlightByFlightNumber_Failure_ArgumentException_400()
        {
            Mock<FlightService> service = new Mock<FlightService>();
            service.Setup(s => s.GetFlightByFlightNumber(1))
                .Throws(new ArgumentException());

            FlightController controller = new FlightController(service.Object);
            ObjectResult response = await controller.GetFlightByFlightNumber(1) as ObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("Bad request.", response.Value);
        }
    }
}
