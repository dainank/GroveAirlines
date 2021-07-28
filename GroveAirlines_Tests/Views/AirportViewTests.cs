using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using GroveAirlines.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GroveAirlines_Tests.Views
{
    [TestClass]
    public class AirportViewTests
    {
        [TestMethod]
        public void Constructor_AirportView_Success()
        {
            int airportId = 1;
            string city = "Amsterdam";
            string iata = "AMS";

            AirportView airportView = new AirportView(airportId, city, iata);

            Assert.IsNotNull(airportView);

            Assert.AreEqual(airportView.AirportId, airportId);
            Assert.AreEqual(airportView.City, city);
            Assert.AreEqual(airportView.Iata, iata);
        }
    }
}
