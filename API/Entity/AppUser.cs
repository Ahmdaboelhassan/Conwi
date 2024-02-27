using System.ComponentModel.DataAnnotations;
using API.Helper;
using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class AppUser : IdentityUser
    {   
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
       
        public DateOnly DateOfBirth {get;set;}
        public string? PhotoURL { get; set; }
    }
}