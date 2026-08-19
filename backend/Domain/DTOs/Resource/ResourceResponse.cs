namespace Domain.DTOs.Resource
{
    public class ResourceResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public int Capacity { get; set; }

        public bool IsActive { get; set; }
    }
}
