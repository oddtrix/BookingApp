using Domain.DTOs.Booking;

namespace ApplicationCore.Interfaces
{
    public interface IBookingService
    {
        Task<bool> CancelAsync(Guid id);

        Task<BookingResponse> CreateAsync(CreateBookingRequest request);

        Task<List<BookingResponse>> GetAllAsync(Guid resourceId);
    }
}