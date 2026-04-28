using AntiCafe.BLL.DTOs;

namespace AntiCafe.BLL.Interfaces
{
    public interface IBookingService
    {
        Task<bool> IsRoomAvailable(int roomId, DateTime start, DateTime end);

        Task CreateBookingAsync(BookingDto booking);

        Task<IEnumerable<BookingDto>> GetBookingsAsync();
    }
}
