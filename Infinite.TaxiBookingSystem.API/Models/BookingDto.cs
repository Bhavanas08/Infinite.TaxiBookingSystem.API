using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.TaxiBookingSystem.API.Models
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public int CustomerID { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime TripDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string SourceAddress { get; set; }
        public string DestinationAddress { get; set; }
        public int TaxiId { get; set; }
        public string CustomerName { get; set; }
    }
}
