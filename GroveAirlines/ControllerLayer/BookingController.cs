using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GroveAirlines.ControllerLayer.JSONData;
using GroveAirlines.Exceptions;
using GroveAirlines.ServiceLayer;
using Microsoft.AspNetCore.Mvc;

namespace GroveAirlines.ControllerLayer
{
    [Route("{controller}")]
    public class BookingController : Controller
    {
        private readonly BookingService _bookingService;

        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("{flightNumber}")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingData body, int flightNumber)
        {
            if (ModelState.IsValid && flightNumber.IsPositiveInteger()) // uses interface
            {
                string customerName = $"{body.FirstName} {body.LastName}";
                (bool result, Exception exception) = await _bookingService.CreateBooking(customerName, flightNumber);

                if (result && exception == null)
                {
                    return StatusCode((int)HttpStatusCode.Created);
                }

                return exception is CouldNotAddBookingToDatabaseException   // check
                    ? StatusCode((int)HttpStatusCode.NotFound) : StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

            return StatusCode((int) HttpStatusCode.InternalServerError, ModelState.Root.Errors.First().ErrorMessage);
        }
    }
}
