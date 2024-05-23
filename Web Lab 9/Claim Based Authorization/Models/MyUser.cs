using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class MyUser : IdentityUser
    {
        [Required(ErrorMessage = "Department is required.")] // Add this line
        public required string Department { get; set; }
    }
}
