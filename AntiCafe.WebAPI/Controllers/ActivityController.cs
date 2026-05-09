using Microsoft.AspNetCore.Mvc;
using AntiCafe.BLL.Interfaces;

namespace AntiCafe.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService activityService;

        public ActivityController(IActivityService activityService)
        {
            this.activityService = activityService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var activities = await activityService.GetAllActivitiesAsync();
            return Ok(activities);
        }
    }
}
