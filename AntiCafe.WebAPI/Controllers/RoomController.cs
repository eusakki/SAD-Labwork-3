using Microsoft.AspNetCore.Mvc;
using AntiCafe.BLL.Interfaces;


namespace AntiCafe.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService roomService;

        public RoomController(IRoomService roomService)
        {
            this.roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rooms = await roomService.GetAllRoomsAsync();
            return Ok(rooms);
        }
    }
}
