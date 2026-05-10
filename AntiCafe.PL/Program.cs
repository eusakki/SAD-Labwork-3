using AntiCafe.ConsoleMenu.ApiClients;
using AntiCafe.ConsoleMenu.Menu;

namespace AntiCafe.ConsoleMenu
{
    class Program
    {
        static async Task Main()
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:7187/api/")
            };

            var roomClient = new RoomApiClient(httpClient);
            var bookingClient = new BookingApiClient(httpClient);
            var activityClient = new ActivityApiClient(httpClient);

            // Menu initialization with services
            var actionHandler = new MenuActionHandler(
                roomClient,
                bookingClient,
                activityClient);

            // Run the main menu
            var menu = new MainMenu(actionHandler);
            await menu.Run();
        }
    }
}
