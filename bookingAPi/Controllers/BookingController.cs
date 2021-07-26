
//controller I built from scratch 



//using bookingAPi.Models;
//using bookingAPi.Repositories;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace bookingAPi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BookingController : ControllerBase
//    {
//        private readonly IBookingRepository _bookingRepository;

//        public BookingController(IBookingRepository bookingRepository)
//        {
//            _bookingRepository = bookingRepository;
//        }
//        [HttpGet] //method will handle http gets
//        public async Task<IEnumerable<Booking>> GetBookings()
//        {
//            return await _bookingRepository.Get();
//        }
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Booking>> GetBooking(int id)
//        {
//            return await _bookingRepository.Get(id);

//        }

//        [HttpPost]
//        public async Task<ActionResult<Booking>> PostBooking([FromBody] Booking booking)
//        {
//            var newBooking = await _bookingRepository.Create(booking);
//            return CreatedAtAction(nameof(GetBooking), new { id = newBooking.ID }, newBooking);
//        }

//        [HttpPut]
//        public async Task<ActionResult> PutBooking(int id, [FromBody] Booking booking)
//        {
//            if (id != booking.ID)
//            {
//                return BadRequest();
//            }

//            await _bookingRepository.Update(booking);

//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public async Task<ActionResult> Delete(int id)
//        {
//            var bookingToDelete = await _bookingRepository.Get(id);
//            if (bookingToDelete == null)
//                return NotFound();

//            await _bookingRepository.Delete(bookingToDelete.ID);
//            return NoContent();

//        }
//        [HttpOptions]
//        public string GetNoOfVacancies(DateTime dateTime)

//        {
//            return  "big test";
//        }
//    }
//}
