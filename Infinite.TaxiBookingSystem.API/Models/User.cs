using System.ComponentModel.DataAnnotations;
namespace Infinite.TaxiBookingSystem.API.Models
{
    public class User:LoginModel
    {
        public int Id { get; set; }
       

        public int EmployeeID { get; set; }

        public int CustomerID { get; set; }

        [Required]
        public string Role { get; set; }

        public Employee Employee { get; set; }
        public Customer Customer{ get; set; }
    }
    public class LoginModel
    {
        [Required]
        public string LoginID { get; set; }

        
        [Required]
        public string Password { get; set; }
    }
}
