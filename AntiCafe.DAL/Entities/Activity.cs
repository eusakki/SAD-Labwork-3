namespace AntiCafe.DAL.Entities
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Booking> Bookings { get; set; } = new();
    }
}
