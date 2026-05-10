using AntiCafe.ConsoleMenu.ApiClients;
using AntiCafe.Contracts.DTOs;

namespace AntiCafe.ConsoleMenu.Menu
{
    public class MenuActionHandler
    {
        private readonly RoomApiClient roomClient;
        private readonly BookingApiClient bookingClient;
        private readonly ActivityApiClient activityClient;

        public MenuActionHandler(
            RoomApiClient roomClient,
            BookingApiClient bookingClient,
            ActivityApiClient activityClient)
        {
            this.roomClient = roomClient;
            this.bookingClient = bookingClient;
            this.activityClient = activityClient;
        }

        public async Task ShowRooms()
        {
            var rooms = await roomClient.GetAllRoomsAsync();
            Console.WriteLine("\nRooms:");
            foreach (var r in rooms)
                Console.WriteLine($"{r.Id}: {r.Name} (Capacity: {r.Capacity})");
        }

        public async Task ShowBookings()
        {
            var bookings = await bookingClient.GetAllBookingsAsync();

            Console.WriteLine("\nBookings:");
            foreach (var b in bookings)
            {
                Console.Write($"{b.Id}. Room {b.RoomId}: {b.StartTime} - {b.EndTime} | FullService: {b.IsFullService} | ");
                if (b.Activities != null && b.Activities.Any())
                {
                    Console.Write("Activities:");

                    foreach (var act in b.Activities)
                    {
                        Console.Write($" - {act.Name}");
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Activities: none");
                }
            }
        }

        public async Task CreateBooking()
        {
            try
            {
                Console.Write("RoomId: ");
                int roomId = int.Parse(Console.ReadLine());

                Console.Write("Start (yyyy-MM-dd HH:mm): ");
                DateTime start = DateTime.Parse(Console.ReadLine());

                Console.Write("End (yyyy-MM-dd HH:mm): ");
                DateTime end = DateTime.Parse(Console.ReadLine());

                Console.Write("Full service? (y/n): ");
                bool isFullService = Console.ReadLine()?.ToLower() == "y";

                var activities = isFullService ? new List<ActivityDto>() : ChooseActivities();

                await bookingClient.CreateBookingAsync(new BookingDto
                {
                    RoomId = roomId,
                    StartTime = start,
                    EndTime = end,
                    IsFullService = isFullService,
                    Activities = activities
                });

                Console.WriteLine("\nBooking created successfully!");
            }
            catch (FormatException)
            {
                Console.WriteLine("\n Error: Invalid input format. Please enter numbers and dates correctly.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task UpdateBooking()
        {
            try
            {
                Console.Write("Enter Booking Id to update: ");
                int id = int.Parse(Console.ReadLine());

                var existing = await bookingClient.GetByIdAsync(id);

                if (existing == null)
                {
                    Console.WriteLine("Booking not found.");
                    return;
                }

                Console.WriteLine($"Current booking: {existing.StartTime} - {existing.EndTime}");

                Console.Write("New Start (yyyy-MM-dd HH:mm): ");
                DateTime start = DateTime.Parse(Console.ReadLine());

                Console.Write("New End (yyyy-MM-dd HH:mm): ");
                DateTime end = DateTime.Parse(Console.ReadLine());

                Console.Write("Full service? (y/n): ");
                bool isFullService = Console.ReadLine()?.ToLower() == "y";

                var activities = isFullService ? new List<ActivityDto>() : ChooseActivities(); 

                await bookingClient.UpdateBookingAsync(id, new BookingDto
                {
                    RoomId = existing.RoomId,
                    StartTime = start,
                    EndTime = end,
                    IsFullService = isFullService,
                    Activities = activities
                });

                Console.WriteLine("Booking updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task DeleteBooking()
        {
            try
            {
                Console.Write("Enter Booking Id to delete: ");
                int id = int.Parse(Console.ReadLine());

                await bookingClient.DeleteBookingAsync(id);

                Console.WriteLine("Booking deleted successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task ShowActivities()
        {
            var activities = await activityClient.GetAllActivitiesAsync();
            Console.WriteLine("\nAvaliable activities in the Anti Cafe:");
            foreach (var a in activities)
                Console.WriteLine($"{a.Id}. {a.Name}");
        }

        private List<ActivityDto> ChooseActivities()
        {
            var activities = new List<ActivityDto>();

            while (true)
            {
                Console.WriteLine("\nChoose activity:");
                Console.WriteLine("1. Movie");
                Console.WriteLine("2. Sport");
                Console.WriteLine("3. Board Games");
                Console.WriteLine("4. Console Games");
                Console.Write("Your choice: ");

                int choice = int.Parse(Console.ReadLine());

                string activity = choice switch
                {
                    1 => "Movie",
                    2 => "Sport",
                    3 => "Board Games",
                    4 => "Console Games",
                    _ => throw new Exception("Invalid activity choice.")
                };

                if (!activities.Any(a => a.Name == activity))
                    activities.Add(new ActivityDto { Name = activity });

                Console.Write("Add more activities? (y/n): ");
                if (Console.ReadLine()?.ToLower() != "y")
                    break;
            }

            return activities;
        }
    }
}
