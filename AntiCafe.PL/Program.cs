using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AntiCafe.DAL.Data;
using AntiCafe.DAL.UnitOfWork;
using AntiCafe.BLL.Services;
using AntiCafe.BLL.Interfaces;
using AntiCafe.BLL.Mapping;
using AntiCafe.PL.Menu;
using AntiCafe.PL.Data;

namespace AntiCafe.PL
{
    class Program
    {
        static async Task Main()
        {
            var options = new DbContextOptionsBuilder<AntiCafeDbContext>()
                .UseInMemoryDatabase("AntiCafeDB")
                .Options;

            var context = new AntiCafeDbContext(options);
            var uow = new UnitOfWork(context);

            // Seed initial data
            await DataSeeder.SeedAsync(uow);

            // AutoMapper configuration
            var mapperConfig = new MapperConfiguration(cfg =>
                cfg.AddProfile<MappingProfile>());
            var mapper = mapperConfig.CreateMapper();

            // Services initialization
            IRoomService roomService = new RoomService(uow, mapper);
            IBookingService bookingService = new BookingService(uow, mapper);
            IActivityService activityService = new ActivityService(uow, mapper);

            // Menu initialization with services
            var actionHandler = new MenuActionHandler(
                roomService,
                bookingService,
                activityService);

            // Run the main menu
            var menu = new MainMenu(actionHandler);          
            await menu.Run();
        }
    }
}
