using System.ComponentModel.DataAnnotations;

namespace Application.DTO.Request;

public class LikePost
{
    [Required]
    public int postId { get; set; }
    [Required]
    public string userId { get; set; }

}
