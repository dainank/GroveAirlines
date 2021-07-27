using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GroveAirlines.Exceptions;
using GroveAirlines.ServiceLayer;
using GroveAirlines.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroveAirlines.ControllerLayer
{
    [Route("{controller}")]
    public class FlightController : Controller
    {
        private readonly FlightService _flightService;

        public FlightController(FlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllFlights()
        {
            try
            {
                Queue<FlightView> flights = new Queue<FlightView>();
                await foreach (FlightView flight in _flightService.GetFlights())
                {
                    flights.Enqueue(flight);
                }

                return StatusCode((int) HttpStatusCode.OK, flights);
            }
            catch (FlightNotFoundException)
            {
                return StatusCode((int) HttpStatusCode.NotFound, "No flights were found in the database.");
            }
            catch (Exception)
            {
                return StatusCode((int) HttpStatusCode.BadRequest, "Bad request.");
            }
        }

        [HttpGet("{flightNumber}")]    // GET /flight/{flightNumber}
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFlightByFlightNumber(int flightNumber)
        {
            try
            {
                if (!flightNumber.IsPositiveInteger())
                {
                    throw new Exception();
                }
                FlightView flight = await _flightService.GetFlightByFlightNumber(flightNumber);
                return StatusCode((int) HttpStatusCode.OK, flight);
            }
            catch (FlightNotFoundException)
            {
                return StatusCode((int) HttpStatusCode.NotFound, "The flight was not found in the database.");
            }
            catch (Exception)
            {
                return StatusCode((int) HttpStatusCode.BadRequest, "Bad request.");
            }
        }
    }
}

    