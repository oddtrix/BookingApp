namespace Domain.DTOs.Booking
{
    public class CreateBookingRequest
    {
        public Guid ResourceId { get; set; }

        public string Username { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
