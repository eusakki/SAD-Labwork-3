using Microsoft.AspNetCore.Mvc;
using AntiCafe.BLL.Interfaces;
using AntiCafe.BLL.DTOs;

namespace AntiCafe.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService bookingService;

        public BookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await bookingService.GetBookingsAsync();
            return Ok(bookings);
        }

        // GET by id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var booking = await bookingService.GetByIdAsync(id);
            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookingDto dto)
        {
            await bookingService.CreateBookingAsync(dto);
            return Ok("Created successfully!");
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookingDto dto)
        {
            await bookingService.UpdateBookingAsync(id, dto);
            return Ok("Updated successfully!");
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await bookingService.DeleteBookingAsync(id);
            return Ok("Deleted successfully!");
        }
    }
}