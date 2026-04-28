using AntiCafe.BLL.DTOs;

namespace AntiCafe.BLL.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetAllRoomsAsync();
    }
}
