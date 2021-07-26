using bookingAPi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookingAPi.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingContext _context;

        public BookingRepository(BookingContext context)
        {
            _context = context;
        }

        public async Task<Booking> Create(Booking booking)
        {
            _context.Booking.Add(booking);
            await _context.SaveChangesAsync();

            return booking;
        }

        //deleting bookings by id
        public async Task Delete(int id)
        {
            var bookingToDelete = await _context.Booking.FindAsync(id);
            _context.Booking.Remove(bookingToDelete);
            await _context.SaveChangesAsync();
        }


        //creating a list of bookings 
        public async Task<IEnumerable<Booking>> Get()
        {
            return await _context.Booking.ToListAsync();
        }

        //finding booking by ID
        public async Task<Booking> Get(int id)
        {
            return await _context.Booking.FindAsync(id); 
        }
        // updating booking
        public async Task Update(Booking booking)
        {
            _context.Entry(booking).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        
    }
}
