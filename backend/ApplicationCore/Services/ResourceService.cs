using ApplicationCore.Interfaces;
using Domain.DTOs.Resource;
using Domain.Entities;
using Domain.Enums;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApplicationCore.Services
{
    public class ResourceService : IResourceService
    {
        private readonly Context _dbContext;

        public ResourceService(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResourceResponse> CreateAsync(CreateResourceRequest createResource)
        {
            Enum.TryParse(createResource.Type, out ResourceType resourceType);

            var resourceEntity = new Resource
            {
                Name = createResource.Name,
                Type = resourceType,
                Capacity = createResource.Capacity,
                IsActive = true
            };

            _dbContext.Resources.Add(resourceEntity);
            await _dbContext.SaveChangesAsync();

            return new ResourceResponse
            {
                Id = resourceEntity.Id,
                Name = resourceEntity.Name,
                Type = resourceEntity.Type.ToString(),
                Capacity = resourceEntity.Capacity,
                IsActive = resourceEntity.IsActive
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var resourceEntity = await _dbContext.Resources.FirstOrDefaultAsync(r => r.Id == id);

            if (resourceEntity is not null)
            {
                resourceEntity.IsActive = false;
                await _dbContext.SaveChangesAsync();
                
                return true;
            }

            return false;
        }

        public async Task<List<ResourceResponse>> GetAllAsync()
        {
            var resources =  await _dbContext.Resources
                .Select(r => new ResourceResponse
                {
                    Id = r.Id,
                    Name = r.Name,
                    Type = r.Type.ToString(),
                    Capacity = r.Capacity,
                    IsActive = r.IsActive
                }).ToListAsync();

            return resources;
        }

        public async Task<ResourceResponse?> GetByIdAsync(Guid id)
        {
            var resourceEntity = await _dbContext.Resources.FirstOrDefaultAsync(r => r.Id == id);

            if (resourceEntity is not null)
            {
                return new ResourceResponse
                {
                    Id = resourceEntity.Id,
                    Name = resourceEntity.Name,
                    Type = resourceEntity.Type.ToString(),
                    Capacity = resourceEntity.Capacity,
                    IsActive = resourceEntity.IsActive
                };
            }

            return null;
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateResourceRequest updateResource)
        {
            var resourceEntity = await _dbContext.Resources.FirstOrDefaultAsync(r => r.Id == id);

            if (resourceEntity is not null)
            {
                resourceEntity.Name = updateResource.Name;
                Enum.TryParse(updateResource.Type, out ResourceType resourceType);
                resourceEntity.Type = resourceType;
                resourceEntity.Capacity = updateResource.Capacity;
                resourceEntity.IsActive = updateResource.IsActive;

                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
