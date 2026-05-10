using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AntiCafe.Contracts.DTOs;
using AntiCafe.BLL.Interfaces;
using AntiCafe.DAL.Entities;
using AntiCafe.DAL.UnitOfWork;
using AntiCafe.DAL.Data;

namespace AntiCafe.BLL.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly AntiCafeDbContext context;

        public BookingService(IUnitOfWork uow, IMapper mapper, AntiCafeDbContext context)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<bool> IsRoomAvailable(int roomId, DateTime start, DateTime end, int? excludeBookingId = null)
        {
            var bookings = await uow.Bookings.FindAsync(b =>
                b.RoomId == roomId &&
                (excludeBookingId == null || b.Id != excludeBookingId) &&
                !(end <= b.StartTime || start >= b.EndTime)
            );

            return !bookings.Any();
        }

        public async Task CreateBookingAsync(BookingDto bookingDto)
        {
            if (bookingDto.StartTime >= bookingDto.EndTime)
                throw new Exception("Start time must be before end time.");

            if (bookingDto.StartTime < DateTime.Now)
                throw new Exception("Start time cannot be in the past.");

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
                booking.Activities = await GetRandomActivitiesAsync();
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
            var bookings = await context.Bookings
                .Include(b => b.Activities)
                .ToListAsync();

            return mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<BookingDto> GetByIdAsync(int id)
        {
            var booking = await context.Bookings
                .Include(b => b.Activities)
                .FirstOrDefaultAsync(b => b.Id == id);

            return mapper.Map<BookingDto>(booking);
        }
        
        public async Task UpdateBookingAsync(int id, BookingDto dto)
        {
            var entity = await context.Bookings
                .Include(b => b.Activities)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (entity == null)
                throw new Exception("Booking not found.");

            if (dto.StartTime >= dto.EndTime)
                throw new Exception("Start time must be before end time.");

            if (dto.StartTime < DateTime.Now)
                throw new Exception("Start time cannot be in the past.");

            bool available = await IsRoomAvailable(
                dto.RoomId, 
                dto.StartTime, 
                dto.EndTime,
                id);

            if (!available)
                throw new Exception("Room is not available in this time.");

            entity.RoomId = dto.RoomId;
            entity.StartTime = dto.StartTime;
            entity.EndTime = dto.EndTime;
            entity.IsFullService = dto.IsFullService;

            entity.Activities.Clear();

            if (dto.IsFullService)
            {
                var randomActivities = await GetRandomActivitiesAsync();

                foreach (var act in randomActivities)
                {
                    var attached = await uow.Activities.GetByIdAsync(act.Id);
                    entity.Activities.Add(attached);
                }
            }
            else
            {
                if (dto.Activities == null || !dto.Activities.Any())
                    throw new Exception("You must select at least one activity.");

                var allActivities = await uow.Activities.GetAllAsync();

                foreach (var dtoActivity in dto.Activities)
                {
                    var activity = allActivities
                        .FirstOrDefault(a => a.Name == dtoActivity.Name);

                    if (activity != null)
                        entity.Activities.Add(activity);
                }
            }

            await uow.SaveAsync();
        }

        public async Task DeleteBookingAsync(int id)
        {
            var entity = await uow.Bookings.GetByIdAsync(id);

            if (entity == null)
                throw new Exception("Booking not found.");

            entity.Activities.Clear();

            uow.Bookings.Delete(entity);
            await uow.SaveAsync();
        }

        private async Task<List<Activity>> GetRandomActivitiesAsync()
        {
            var AllACtivities = await uow.Activities.GetAllAsync();
            var random = new Random();

            int totalAvailable = AllACtivities.Count();
            int maxToTake = Math.Min(5, totalAvailable + 1);

            int countToTake = random.Next(2, maxToTake);
            return AllACtivities
                .OrderBy(x => random.Next())
                .Take(countToTake)
                .ToList();
        }
    }
}
