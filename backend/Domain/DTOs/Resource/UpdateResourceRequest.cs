namespace Domain.DTOs.Resource
{
    public class UpdateResourceRequest
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public int Capacity { get; set; }

        public bool IsActive { get; set; }
    }
}
