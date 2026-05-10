using AntiCafe.Contracts.DTOs;

namespace AntiCafe.ConsoleMenu.ApiClients
{
    public class BookingApiClient : BaseApiClient
    {
        public BookingApiClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<List<BookingDto>> GetAllBookingsAsync()
        {
            return await GetAsync<List<BookingDto>>("bookings");
        }

        public async Task<BookingDto?> GetByIdAsync(int id)
        {
            return await GetAsync<BookingDto>($"bookings/{id}");
        }

        public async Task CreateBookingAsync(BookingDto dto)
        {
            await PostAsync("bookings", dto);
        }

        public async Task UpdateBookingAsync(int id, BookingDto dto)
        {
            await PutAsync($"bookings/{id}", dto);
        }

        public async Task DeleteBookingAsync(int id)
        {
            await base.DeleteAsync($"bookings/{id}");
        }
    }
}
