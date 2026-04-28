using Microsoft.EntityFrameworkCore;
using AntiCafe.DAL.Entities;


namespace AntiCafe.DAL.Data
{
    public  class AntiCafeDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public AntiCafeDbContext(DbContextOptions<AntiCafeDbContext> options)
            : base(options)
        {
        }
    }
}
