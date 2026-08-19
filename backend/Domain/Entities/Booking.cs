using Domain.Enums;

namespace Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; set; }

        public Guid ResourceId { get; set; }

        public string Username { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public BookingStatus Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Resource Resource { get; set; }
    }
}
