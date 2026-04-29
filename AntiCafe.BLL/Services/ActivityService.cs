using AutoMapper;
using AntiCafe.BLL.DTOs;
using AntiCafe.BLL.Interfaces;
using AntiCafe.DAL.UnitOfWork;

namespace AntiCafe.BLL.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public ActivityService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ActivityDto>> GetAllActivitiesAsync()
        {
            var activities = await uow.Activities.GetAllAsync();
            return mapper.Map<IEnumerable<ActivityDto>>(activities);
        }
    }
}
