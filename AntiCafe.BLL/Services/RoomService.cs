using AutoMapper;
using AntiCafe.BLL.DTOs;
using AntiCafe.BLL.Interfaces;
using AntiCafe.DAL.UnitOfWork;

namespace AntiCafe.BLL.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public RoomService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<RoomDto>> GetAllRoomsAsync()
        {
            var rooms = await uow.Rooms.GetAllAsync();
            return mapper.Map<IEnumerable<RoomDto>>(rooms);
        }
    }
}
