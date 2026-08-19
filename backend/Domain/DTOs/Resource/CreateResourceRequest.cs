namespace Domain.DTOs.Resource
{
    public class CreateResourceRequest
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public int Capacity { get; set; }
    }
}
