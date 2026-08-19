using ApplicationCore.Interfaces;
using Domain.DTOs.Resource;
using Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourcesController : ControllerBase
    {
        private readonly IResourceService _resourceService;

        public ResourcesController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        [HttpGet]
        public async Task<ActionResult<ResourceResponse>> GetAll()
        {
            var result = await _resourceService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ResourceResponse>> Get(Guid id)
        {
            var result = await _resourceService.GetByIdAsync(id);

            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateResourceRequest createResource)
        {
            if (string.IsNullOrWhiteSpace(createResource.Name) || createResource.Capacity <= 0)
            {
                return BadRequest(ErrorMessages.InvalidResource);
            }

            var result = await _resourceService.CreateAsync(createResource);

            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] UpdateResourceRequest updateResource)
        {
            var result = await _resourceService.UpdateAsync(id, updateResource);

            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _resourceService.DeleteAsync(id);

            return result ? NoContent() : NotFound();
        }
    }
}
