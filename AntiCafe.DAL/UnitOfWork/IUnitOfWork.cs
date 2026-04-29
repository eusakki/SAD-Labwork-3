using AntiCafe.DAL.Repositories;
using AntiCafe.DAL.Entities;

namespace AntiCafe.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Room> Rooms { get; }
        IRepository<Booking> Bookings { get; }
        IRepository<Activity> Activities { get; }

        Task<int> SaveAsync();
    }
}
