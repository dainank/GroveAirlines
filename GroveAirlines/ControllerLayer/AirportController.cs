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
    public class AirportController : Controller
    {
        private readonly AirportService _airportService;

        public AirportController(AirportService airportService)
        {
            _airportService = airportService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAirports()
        {
            try
            {
                Queue<AirportView> airports = new Queue<AirportView>();
                await foreach (AirportView airport in _airportService.GetAllAirports())
                {
                    airports.Enqueue(airport);
                }

                return StatusCode((int) HttpStatusCode.OK, airports);
            }
            catch (AirportNotFoundException)
            {
                return StatusCode((int)HttpStatusCode.NotFound, "No airports were found in the database.");
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "Bad request.");
            }
        }
    }
}
