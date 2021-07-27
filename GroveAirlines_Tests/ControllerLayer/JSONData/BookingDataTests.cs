using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroveAirlines.ControllerLayer.JSONData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GroveAirlines_Tests.ControllerLayer.JSONData
{
    [TestClass]
    public class BookingDataTests
    {
        [TestMethod]
        public void BookingData_ValidData()
        {
            BookingData bookingData = new BookingData
            {
                FirstName = "Benjamin",
                LastName = "Whelan"
            };
            Assert.AreEqual("Benjamin", bookingData.FirstName);
            Assert.AreEqual("Whelan", bookingData.LastName);
        }

        [TestMethod]
        [DataRow("Scott", null)]
        [DataRow(null, "Whelan")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void BookingData_InvalidData_NullPointers(string firstName, string lastName)
        {
            BookingData bookingData = new BookingData
            {
                FirstName = firstName,
                LastName = lastName
            };
            Assert.AreEqual(firstName, bookingData.FirstName);
            Assert.AreEqual(lastName, bookingData.LastName);
        }

        [TestMethod]
        [DataRow("Scott", "")]
        [DataRow("", "Whelan")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void BookingData_InvalidData_EmptyStrings(string firstName, string lastName)
        {
            BookingData bookingData = new BookingData
            {
                FirstName = firstName,
                LastName = lastName
            };
            Assert.AreEqual(firstName, bookingData.FirstName ?? "");
            Assert.AreEqual(lastName, bookingData.LastName ?? "");
        }
    }
}
