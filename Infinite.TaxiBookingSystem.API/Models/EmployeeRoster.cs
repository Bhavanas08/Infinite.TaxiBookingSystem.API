using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.TaxiBookingSystem.API.Models
{
    public class EmployeeRoster
    {
        //RosterID, EmployeeID, FromDate, ToDate, InTime, OutTime
       
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }

        //Navigation Property
        public Employee Employee { get; set; }

        //ForeignKey
        public int EmployeeId { get; set; }
    }
}
