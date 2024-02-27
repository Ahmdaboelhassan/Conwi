using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public record class Register
    {
        [Required , StringLength(20)]
        [RegularExpression("^[A-Z]?[A-Za-z]+")]
        public string FirstName { get; set; }

        [Required,StringLength(20)]
        [RegularExpression("^[A-Z]?[A-Za-z]+")]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        public DateOnly DateOfBirth {get;set;}

        [Required]
        [DataType(DataType.Password)]
        public string Password {get;set;}

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfrimPassword {get;set;}
        
    }
}