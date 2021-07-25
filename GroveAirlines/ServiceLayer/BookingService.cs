using GroveAirlines.DatabaseLayer.Models;
using GroveAirlines.Exceptions;
using GroveAirlines.RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace GroveAirlines.ServiceLayer
{
    public class BookingService
    {
        private readonly BookingRepository _bookingRepository;
        private readonly FlightRepository _flightRepository;
        private readonly CustomerRepository _customerRepository;

        public BookingService(BookingRepository repository)
        {
            _bookingRepository = repository;
        }

        public BookingService(BookingRepository bookingRepository, CustomerRepository customerRepository)
        {
            _bookingRepository = bookingRepository;
            _customerRepository = customerRepository;
        }

        public BookingService(BookingRepository bookingRepository, FlightRepository flightRepository, CustomerRepository customerRepository)
        {
            _bookingRepository = bookingRepository;
            _flightRepository = flightRepository;
            _customerRepository = customerRepository;
        }

        public async Task<(bool, Exception)> CreateBooking(string customerName, int flightNumber)
        {
            if (string.IsNullOrEmpty(customerName) || !flightNumber.IsPositiveInteger())
            {
                return (false, new ArgumentException());
            }

            try
            {
                Customer customer = await GetCustomerFromDatabase(customerName) ?? await AddCustomerToDatabase(customerName);

                if (!await FlightExistsInDatabase(flightNumber))
                {
                    return (false, new CouldNotAddBookingToDatabaseException());
                }

                await _bookingRepository.CreateBooking(customer.CustomerId, flightNumber);
                return (true, null);
            }
            catch (Exception exception)
            {
                return (false, exception);
            }
        }

        private async Task<bool> FlightExistsInDatabase(int flightNumber)
        {
            try
            {
                return await _flightRepository.GetFlightByFlightNumber(flightNumber) != null;
            }
            catch (FlightNotFoundException)
            {
                return false;
            }
        }

        private async Task<Customer> GetCustomerFromDatabase(string name)
        {
            try
            {
                return await _customerRepository.GetCustomerByName(name);
            }
            catch (CustomerNotFoundException)
            {
                return null;
            }
            catch (Exception exception)
            {
                ExceptionDispatchInfo.Capture(exception.InnerException ?? new Exception()).Throw();
                return null;
            }
        }

        private async Task<Customer> AddCustomerToDatabase(string name)
        {
            await _customerRepository.CreateCustomer(name);
            return await _customerRepository.GetCustomerByName(name);
        }
    }
}
