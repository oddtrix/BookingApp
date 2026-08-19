using Domain.Enums;

namespace Domain.Entities
{
    public class Resource
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ResourceType Type { get; set; }

        public int Capacity { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
