using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookingAPi.Models
{
    public class Response
    {
        public bool Success;

        public List<string> Errors = new List<string>();

        public string Payload;

        public DateTime TimeStamp;
    }
}
