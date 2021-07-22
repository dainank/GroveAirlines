namespace GroveAirlines.DatabaseLayer.Models
{
    public sealed class Booking
    {
        public int BookingId { get; set; }
        public int FlightNumber { get; set; }
        public int? CustomerId { get; set; }

        public Customer Customer { get; set; }  // each booking has a customer
        public Flight FlightNumberNavigation { get; set; }  // connects to NumberNavigation
    }
}
