using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bookingAPi.Models;
using bookingAPi.Repositories;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace bookingAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController1 : ControllerBase
    {
        private readonly BookingContext _context;

        public BookingsController1(BookingContext context)
        {
            _context = context;
        }

        // Create get booking method 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBooking()
        {
            
            var respones = new Response();
            respones.Success = true;
            respones.TimeStamp = DateTime.Now;
            respones.Payload = JsonSerializer.Serialize(await _context.Booking.ToListAsync());
            //var testString = JsonSerializer.Serialize(respones);
            //return Content("test");
            return await
            _context.Booking.ToListAsync();

              /*Content(JsonSerializer.Serialize(respones))*/;
            
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        //Old get method 

        // GET: api/BookingsController1/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Booking>> GetBooking(int id)
        //{
        //    var booking = await _context.Booking.FindAsync(id);

        //    if (booking == null)
        //    {
        //        return NotFound();
        //    }

        //    return booking;
        //}

        // PUT: api/BookingsController1/5

        //add from and to dates
        //new booking system 
        //-------------------------------------------------------------------------------------------------------------------------------------
        
        //Create booking method
        [HttpPost]
        [Route("/CreateBooking")]
        public async Task<ActionResult<Booking>> CreateBooking(DateTime from, DateTime end, string CarReg)
        {
            var newBooking = new Booking();
            newBooking.FromDate = from;
            newBooking.ToDate = end;
            newBooking.CarReg = CarReg;
            newBooking.Cancelled = true;
            newBooking.CreatedTimeStamp = DateTime.Now;

            bool cancelledDueToNoVacancies = false;
            bool cancelledDueToDoubleBooking = false;

            var bookingList = _context.Booking.ToListAsync();

            for (DateTime date = from; date <= end; date = date.AddDays(1))
            {
                var maxVacancies = 10; // would be in database

                foreach (var booking in bookingList.Result)
                {
                    if (date > booking.FromDate
                    && date < booking.ToDate
                    && booking.Cancelled == false)
                    {
                        maxVacancies--;
                        if (maxVacancies <= 0)
                        {
                            cancelledDueToNoVacancies = false;
                        }
                    }
                }
                newBooking.Price +=
                GetPriceForDay(date);

            }
            if (cancelledDueToDoubleBooking == false &&
                cancelledDueToNoVacancies == false
                && newBooking.ToDate >= newBooking.FromDate)
            {
                _context.Booking.Add(newBooking);

                await _context.SaveChangesAsync();
                return Content("success");
            }

            return Content("fail");
        }

        // GET: booking id by car reg
        [HttpGet]
        [Route("/GetBookingByCarReg")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingByCarReg(string carReg)
        {

            return await _context.Booking.Where(a => a.CarReg == carReg).ToListAsync();
        }

        // find number of free spaces for a giving date 
        [HttpGet]
        [Route("/GetNoOfVacancies")]
        public int GetNoOfVacancies(DateTime dateTime)

        {
            var maxVacancies = 10; // would be in database
            var bookingList = _context.Booking.ToListAsync();

            foreach (var booking in bookingList.Result)
            {
                if (dateTime > booking.FromDate
                && dateTime < booking.ToDate
                && booking.Cancelled == false) // 
                {
                    maxVacancies--;
                }
            }
            return maxVacancies;
        }


        // edit booking method
        [HttpPut("{id}")]
        
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.ID)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        //-----------------------------------------------------------------------------------------------------------------------
        // old edit booking method 

        //// edit booking
        //[HttpPost]
        //public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        //{
        //    _context.Booking.Add(booking);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetBooking", new { id = booking.ID }, booking);
        //}

        // delete booking from datebase
        //-------------------------------------------------------------------------------------------------------------------

        // create delete booking method 
        //this method completly removes the booking from the database and would not be used. 
        [HttpDelete("{id}")]
        
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.ID == id);
        }


        // getting parking price
        [HttpGet]
        [Route("/GetParkingPrice")]
        public decimal GetParkingPrice(DateTime datetime)
        {
            var price = 5m;
            if (datetime.Month >= 8 &&
                datetime.Month <= 11)
            {
                price += 3;
            }
            if (datetime.DayOfWeek == DayOfWeek.Saturday ||
                datetime.DayOfWeek == DayOfWeek.Sunday)
            {
                price += 1.5m;
            }


            return price;
        }

        // Cancelled booking (remains on database)
        //this will be used instead of the delete method 
        [HttpPut]
        [Route("/CancelledBooking")]
        public async Task<IActionResult> CancelledBooking(int id)
        {
           var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
           booking.Cancelled = true;
           booking.CancelledTimeStamp = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //creating different price for certain months and weekends
        private decimal GetPriceForDay(DateTime date)
        {
            var price = 5m;
            if (date.Month >= 8 &&
                date.Month <= 11)
            {
                price += 3;
            }
            if (date.DayOfWeek == DayOfWeek.Saturday ||
                date.DayOfWeek == DayOfWeek.Sunday)
            {
                price += 1.5m;
            }
            return price;
        }
    }
}
