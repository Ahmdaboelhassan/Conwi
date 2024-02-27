using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public record class LogIn
    {   
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password {get;set;}

    }
}