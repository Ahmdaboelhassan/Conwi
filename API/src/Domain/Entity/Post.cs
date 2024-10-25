using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class Post
    {
        public int id { get; set; }
        [Required]
        public DateTime timePosted { get; set; }
        [Required]
        [MaxLength(100)]
        public string content { get; set; }
        public string? photoURL { get; set; }
        [Required]
        public string UserPostedId { get; set; }
        [ForeignKey(nameof(UserPostedId))]
        public AppUser UserPosted { get; set; }
    }
}