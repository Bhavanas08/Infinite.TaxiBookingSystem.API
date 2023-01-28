using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.TaxiBookingSystem.API.Models
{
    public class Taxi
    {
        public int TaxiId { get; set; }
        public string TaxiModel { get; set; }
        public string Color { get; set; }
        public string RegistrationNumber { get; set; }
        public string TaxiType { get; set; }
    }
}
