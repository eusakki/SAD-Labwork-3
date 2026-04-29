using AntiCafe.DAL.Entities;

namespace AntiCafe.BLL.DTOs
{
    public class ActivityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<BookingDto> Bookings { get; set; } = new();
    }
}
