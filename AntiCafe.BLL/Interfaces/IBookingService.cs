using AntiCafe.Contracts.DTOs;

namespace AntiCafe.BLL.Interfaces
{
    public interface IBookingService
    {
        Task<bool> IsRoomAvailable(int roomId, DateTime start, DateTime end, int? excludeBookingId = null);

        Task CreateBookingAsync(BookingDto booking);

        Task<IEnumerable<BookingDto>> GetBookingsAsync();

        Task<BookingDto> GetByIdAsync(int id);

        Task UpdateBookingAsync(int id, BookingDto dto);

        Task DeleteBookingAsync(int id);
    }
}
