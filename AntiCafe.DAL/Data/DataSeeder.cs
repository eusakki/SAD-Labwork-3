using AntiCafe.DAL.Entities;
using AntiCafe.DAL.UnitOfWork;

namespace AntiCafe.DAL.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(IUnitOfWork uow)
        {
            var rooms = await uow.Rooms.GetAllAsync();

            if (rooms.Any())
                return;

            var roomA = new Room { Name = "Room A", Capacity = 3 };
            var roomB = new Room { Name = "Room B", Capacity = 5 };
            var roomC = new Room { Name = "Room C", Capacity = 10 };

            await uow.Rooms.AddAsync(roomA);
            await uow.Rooms.AddAsync(roomB);
            await uow.Rooms.AddAsync(roomC);

            var movie = new Activity { Name = "Movie" };
            var sport = new Activity { Name = "Sport" };
            var boardGames = new Activity { Name = "Board Games" };
            var console = new Activity { Name = "Console Games" };

            await uow.Activities.AddAsync(movie);
            await uow.Activities.AddAsync(sport);
            await uow.Activities.AddAsync(boardGames);
            await uow.Activities.AddAsync(console);

            await uow.SaveAsync();

            await uow.Bookings.AddAsync(new Booking
            {
                RoomId = 1,
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddHours(2),
                IsFullService = false,
                Activities = new List<Activity>()
                {
                    sport,
                    console
                }
            });

            await uow.Bookings.AddAsync(new Booking
            {
                RoomId = 1,
                StartTime = DateTime.Now.AddHours(3),
                EndTime = DateTime.Now.AddHours(5),
                IsFullService = true,
                Activities = new List<Activity>() 
                {
                    sport,
                    console,
                    boardGames
                }
            });

            await uow.Bookings.AddAsync(new Booking
            {
                RoomId = 2,
                StartTime = DateTime.Now.AddHours(4),
                EndTime = DateTime.Now.AddHours(6),
                IsFullService = false,
                Activities = new List<Activity>()
                {
                    boardGames,
                    movie
                }
            });

            await uow.Bookings.AddAsync(new Booking
            {
                RoomId = 3,
                StartTime = DateTime.Now.AddHours(2),
                EndTime = DateTime.Now.AddHours(4),
                IsFullService = false,
                Activities = new List<Activity>()
                {
                    movie,
                }
            });

            await uow.SaveAsync();
        }
    }
}
