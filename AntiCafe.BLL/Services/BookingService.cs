using AutoMapper;
using AntiCafe.BLL.DTOs;
using AntiCafe.BLL.Interfaces;
using AntiCafe.DAL.Entities;
using AntiCafe.DAL.UnitOfWork;

namespace AntiCafe.BLL.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public BookingService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<bool> IsRoomAvailable(int roomId, DateTime start, DateTime end)
        {
            var bookings = await uow.Bookings.FindAsync(b =>
                b.RoomId == roomId &&
                !(end <= b.StartTime || start >= b.EndTime)
            );

            return !bookings.Any();
        }

        public async Task CreateBookingAsync(BookingDto bookingDto)
        {
            bool available = await IsRoomAvailable(
                bookingDto.RoomId,
                bookingDto.StartTime,
                bookingDto.EndTime);

            if (!available)
                throw new Exception("Room is not availabe in this time.");

            var booking = mapper.Map<Booking>(bookingDto);

            booking.Activities = new List<Activity>();

            if (bookingDto.IsFullService)
            {
                var AllACtivities = await uow.Activities.GetAllAsync();

                var random = new Random();

                int totalAvailable = AllACtivities.Count();
                int maxToTake = Math.Min(5, totalAvailable + 1);

                int countToTake = random.Next(2, maxToTake);
                var randomActivities = AllACtivities
                    .OrderBy(x => random.Next())
                    .Take(countToTake)
                    .ToList();

                foreach (var activity in randomActivities)
                {
                    booking.Activities.Add(activity);
                }
            }
            else
            {
                if (bookingDto.Activities == null || !bookingDto.Activities.Any())
                    throw new Exception("You must select at least one activity.");

                var allActivities = await uow.Activities.GetAllAsync();

                foreach (var dtoActivity in bookingDto.Activities)
                {
                    var trackedActivity = allActivities.FirstOrDefault(a => a.Name == dtoActivity.Name);
                    if (trackedActivity != null)
                    {
                        booking.Activities.Add(trackedActivity);
                    }
                }
            }

            await uow.Bookings.AddAsync(booking);
            await uow.SaveAsync();
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsAsync()
        {
            var bookings = await uow.Bookings.GetAllAsync();
            return mapper.Map<IEnumerable<BookingDto>>(bookings);
        }
    }
}
