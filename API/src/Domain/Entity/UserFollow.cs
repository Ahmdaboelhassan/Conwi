using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity;

public class UserFollow
{
    [Key]
    public int Id { get; set;}
    [Required]
    public string SourceUserId { get; set;}
    [Required]
    public string DistinationUserId { get; set;}
    [ForeignKey(nameof(SourceUserId))]
    public AppUser SourceUser { get; set; }  
    [ForeignKey(nameof(DistinationUserId))]
    public AppUser DistinationUser { get; set; }  
}
