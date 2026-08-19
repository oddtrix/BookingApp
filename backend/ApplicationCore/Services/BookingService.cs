using ApplicationCore.Interfaces;
using Domain.DTOs.Booking;
using Domain.Entities;
using Domain.Enums;
using Domain.Errors;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApplicationCore.Services
{
    public class BookingService : IBookingService
    {
        private readonly Context _dbContext;

        public BookingService(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BookingResponse> CreateAsync(CreateBookingRequest request)
        {
            if (request.StartTime >= request.EndTime)
            {
                return new BookingResponse { Error = ErrorMessages.EndTimeMustBeGreaterThanStartTime };
            }

            var resource = await _dbContext.Resources.FirstOrDefaultAsync(r => r.Id == request.ResourceId);

            if (resource is null)
            {
                return new BookingResponse { Error = ErrorMessages.ResourceNotFound };
            }

            if (!resource.IsActive)
            {
                return new BookingResponse { Error = ErrorMessages.ResourceIsInactive };
            }

            await using var transaction = await _dbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable);

            var conflict = await _dbContext.Bookings.AnyAsync(b =>
                                    b.ResourceId == request.ResourceId && b.Status != BookingStatus.Cancelled &&
                                    b.StartTime < request.EndTime && b.EndTime > request.StartTime);

            if (conflict)
            {
                await transaction.RollbackAsync();
                return new BookingResponse { Error = ErrorMessages.SelectedSlotIsBooked };
            }

            var booking = new Booking
            {
                ResourceId = request.ResourceId,
                Username = request.Username,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Status = BookingStatus.Confirmed,
            };

            _dbContext.Bookings.Add(booking);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return new BookingResponse
            {
                Id = booking.Id,
                ResourceId = booking.ResourceId,
                ResourceName = resource.Name,
                Username = booking.Username,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                Status = booking.Status.ToString(),
                CreatedAt = booking.CreatedAt
            };
        }

        public async Task<List<BookingResponse>> GetAllAsync(Guid resourceId)
        {
            return await _dbContext.Bookings
                .Include(b => b.Resource)
                .Where(b => b.ResourceId == resourceId)
                .OrderBy(b => b.StartTime)
                .Select(b => new BookingResponse
                {
                    Id = b.Id,
                    ResourceId = b.ResourceId,
                    ResourceName = b.Resource.Name,
                    Username = b.Username,
                    StartTime = b.StartTime,
                    EndTime = b.EndTime,
                    Status = b.Status.ToString(),
                    CreatedAt = b.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<bool> CancelAsync(Guid id)
        {
            var booking = await _dbContext.Bookings.FirstOrDefaultAsync(b => b.Id == id);

            if (booking is null)
            {
                return false;
            }

            booking.Status = BookingStatus.Cancelled;
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
