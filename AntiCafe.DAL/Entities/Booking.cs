namespace AntiCafe.DAL.Entities
{
    public class Booking
    {
        public int Id { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public bool IsFullService { get; set; }

        public List<Activity> Activities { get; set; } = new();
    }
}
