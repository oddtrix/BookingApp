using Domain.DTOs.Resource;

namespace ApplicationCore.Interfaces
{
    public interface IResourceService
    {
        public Task<List<ResourceResponse>> GetAllAsync();

        public Task<ResourceResponse?> GetByIdAsync(Guid id);

        public Task<ResourceResponse> CreateAsync(CreateResourceRequest createResource);

        public Task<bool> UpdateAsync(Guid id, UpdateResourceRequest updateResource);

        public Task<bool> DeleteAsync(Guid id);
    }
}
