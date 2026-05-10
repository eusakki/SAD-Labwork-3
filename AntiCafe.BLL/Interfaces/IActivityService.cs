using AntiCafe.Contracts.DTOs;

namespace AntiCafe.BLL.Interfaces
{
    public interface IActivityService
    {
        Task<IEnumerable<ActivityDto>> GetAllActivitiesAsync();
    }
}
