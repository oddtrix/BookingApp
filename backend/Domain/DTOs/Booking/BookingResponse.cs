using Domain.Enums;

namespace Domain.DTOs.Booking
{
    public class BookingResponse
    {
        public Guid Id { get; set; }

        public Guid ResourceId { get; set; }

        public string ResourceName { get; set; }

        public string Username { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string Error { get; set; } = string.Empty;
    }
}
