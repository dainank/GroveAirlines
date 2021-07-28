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
    public class AirportControllerTests
    {
        [TestMethod]
        public async Task GetAllAirports_Success()
        {
            Mock<AirportService> service = new Mock<AirportService>(); // instantiate

            List<AirportView> returnAirportViews = new List<AirportView>(2) // create body
            {
                new AirportView(638, "Amsterdam", "AMS"),
                new AirportView(938, "Luxembourg", "LUX")
            };
            // mock returns list of AirportViews
            service.Setup(s => s.GetAllAirports()).Returns(AirportViewAsyncGenerator(returnAirportViews));

            AirportController controller = new AirportController(service.Object);
            ObjectResult response = await controller.GetAllAirports() as ObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual((int) HttpStatusCode.OK, response.StatusCode);

            Queue<AirportView> content = response.Value as Queue<AirportView>;
            Assert.IsNotNull(content);

            Assert.IsTrue(returnAirportViews.All(airport => content.Contains(airport)));
        }

        private async IAsyncEnumerable<AirportView> AirportViewAsyncGenerator(IEnumerable<AirportView> views)
        {
            foreach (AirportView airportView in views)
            {
                yield return airportView;
            }
        }

        [TestMethod] // 404
        public async Task GetAllAirports_Failure_AirportNotFoundException_404()
        {
            Mock<AirportService> service = new Mock<AirportService>(); // no airport views
            service.Setup(s => s.GetAllAirports()).Throws(new AirportNotFoundException());

            AirportController controller = new AirportController(service.Object);
            ObjectResult response = await controller.GetAllAirports() as ObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual((int) HttpStatusCode.NotFound, response.StatusCode);
            Assert.AreEqual("No airports were found in the database.", response.Value);
        }

        [TestMethod] // 500
        public async Task GetAllAirports_Failure_InternalServerError_400()
        {
            Mock<AirportService> service = new Mock<AirportService>();
            service.Setup(s => s.GetAllAirports()).Throws(new ArgumentException());

            AirportController controller = new AirportController(service.Object);
            ObjectResult response = await controller.GetAllAirports() as ObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual((int) HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("Bad request.", response.Value);
        }
    }
}
