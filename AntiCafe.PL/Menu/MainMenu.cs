namespace AntiCafe.PL.Menu
{
    public class MainMenu
    {
        private readonly MenuActionHandler actionHandler;

        public MainMenu(MenuActionHandler actionHandler)
        {
            this.actionHandler = actionHandler;
        }

        public async Task Run()
        {
            while (true)
            {
                Console.WriteLine("\n----- AntiCafe Menu -----");
                Console.WriteLine("1. Show rooms");
                Console.WriteLine("2. Show bookings");
                Console.WriteLine("3. Create booking");
                Console.WriteLine("4. Show all avaliable activities in the cafe");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await actionHandler.ShowRooms();
                        break;
                    case "2":
                        await actionHandler.ShowBookings();
                        break;
                    case "3":
                        await actionHandler.CreateBooking();
                        break;
                    case "4":
                        await actionHandler.ShowActivities();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        continue;
                }
            }
        }
    }
}
