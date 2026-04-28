using AntiCafe.DAL.Entities;
using AntiCafe.DAL.Repositories;
using AntiCafe.DAL.Data;

namespace AntiCafe.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AntiCafeDbContext context;

        public IRepository<Room> Rooms { get; }
        public IRepository<Booking> Bookings { get; }
        public IRepository<Activity> Activities { get; }

        public UnitOfWork(AntiCafeDbContext context)
        {
            this.context = context;

            Rooms = new Repository<Room>(context);
            Bookings = new Repository<Booking>(context);
            Activities = new Repository<Activity>(context);
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
