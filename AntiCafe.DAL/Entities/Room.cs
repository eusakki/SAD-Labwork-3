namespace AntiCafe.DAL.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
