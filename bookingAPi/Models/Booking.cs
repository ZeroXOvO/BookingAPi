using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookingAPi.Models
{
    public class Booking
    {
        public int ID { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string CarReg { get; set; }

        public decimal Price { get; set; }

        public bool Cancelled { get; set; }

        public DateTime CancelledTimeStamp { get; set; }

        public DateTime CreatedTimeStamp { get; set;  }
    }
}
