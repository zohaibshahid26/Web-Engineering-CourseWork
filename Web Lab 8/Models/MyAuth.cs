using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Lab8Web.Models
{
    public class MyAuth : IdentityUser
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
    }
}
