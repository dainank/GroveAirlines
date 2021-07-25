using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroveAirlines.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GroveAirlines_Tests.Views
{
    [TestClass]
    public class FlightViewTests
    {
        [TestMethod]
        public void Constructor_FlightView_Success()
        {
            string flightNumber = "0";
            string originCity = "Amsterdam";
            string originCityCode = "AMS";
            string destinationCity = "Manchester";
            string destinationCityCode = "MAN";

            FlightView flightView = new FlightView(flightNumber, (originCity, originCityCode),
                (destinationCity, destinationCityCode));
            Assert.IsNotNull(flightView);

            Assert.AreEqual(flightView.FlightNumber, flightNumber);
            Assert.AreEqual(flightView.Origin.City, originCity);
            Assert.AreEqual(flightView.Origin.Code, originCityCode);
            Assert.AreEqual(flightView.Destination.City, destinationCity);
            Assert.AreEqual(flightView.Destination.Code, destinationCityCode);
        }

        [TestMethod]
        public void Constructor_FlightView_Success_FlightNumber_Null()
        {
            string originCity = "Roissy-en-France";
            string originCityCode = "CDG";
            string destinationCity = "Narita";
            string destinationCityCode = "NRT";

            FlightView flightView = new FlightView(null, (originCity, originCityCode),
                (destinationCity, destinationCityCode));

            Assert.IsNotNull(flightView);

            Assert.AreEqual(flightView.FlightNumber, "No flight number found.");
            Assert.AreEqual(flightView.Origin.City, originCity);
            Assert.AreEqual(flightView.Destination.City, destinationCity);
        }

        [TestMethod]
        public void Constructor_AirportInfo_Success_City_EmptyString()
        {
            string originCity = String.Empty;
            string originAirportCode = "RMF";

            FlightView.AirportInfo airportInfo = new FlightView.AirportInfo((originCity, originAirportCode));
            Assert.IsNotNull(airportInfo);

            Assert.AreEqual(airportInfo.City, "No city found.");
            Assert.AreEqual(airportInfo.Code, originAirportCode);
        }

        [TestMethod]
        public void Constructor_AirportInfo_Success_AirportCode_EmptyString()
        {
            string originCity = "Marsa Alam";
            string originAirportCode = String.Empty;

            FlightView.AirportInfo airportInfo = new FlightView.AirportInfo((originCity, originAirportCode));
            Assert.IsNotNull(airportInfo);

            Assert.AreEqual(airportInfo.City, originCity);
            Assert.AreEqual(airportInfo.Code, "No airport code found.");
        }
    }
}
