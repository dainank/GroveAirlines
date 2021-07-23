using GroveAirlines.RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroveAirlines.ServiceLayer
{
    public class BookingService
    {

        private readonly BookingRepository _bookingRepository;
        private readonly CustomerRepository _customerRepository;
        private BookingRepository repository;

        public BookingService(BookingRepository repository)
        {
            this.repository = repository;
        }

        public BookingService(BookingRepository bookingRepository, CustomerRepository customerRepository)
        {
            _bookingRepository = bookingRepository;
            _customerRepository = customerRepository;
        }

        public virtual async Task<(bool, Exception)> CreateBooking(string customerName, int flightNumber)
        {
            return (true, null);
        }
    }
}
