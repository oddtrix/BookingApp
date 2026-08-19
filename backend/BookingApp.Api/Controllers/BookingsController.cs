using ApplicationCore.Interfaces;
using Domain.DTOs.Booking;
using Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] Guid resourceId)
        {
            var result = await _bookingService.GetAllAsync(resourceId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateBookingRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
            {
                return BadRequest(ErrorMessages.UsernameIsRequired);
            }

            var result = await _bookingService.CreateAsync(request);

            if (!string.IsNullOrWhiteSpace(result.Error))
            {
                return result.Error == ErrorMessages.SelectedSlotIsBooked
                    ? Conflict(new { message = result.Error })
                    : BadRequest(new { message = result.Error });
            }

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPost("{id:guid}/cancel")]
        public async Task<ActionResult> Cancel(Guid id)
        {
            var result = await _bookingService.CancelAsync(id);

            return result ? NoContent() : NotFound();
        }
    }
}
