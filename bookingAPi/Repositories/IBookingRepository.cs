using bookingAPi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookingAPi.Repositories
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> Get();

        Task<Booking> Get(int id);

        Task<Booking> Create(Booking booking);

        Task Update(Booking booking);

        Task Delete(int id);

    }
}
