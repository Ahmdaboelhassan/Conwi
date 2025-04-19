using System.ComponentModel.DataAnnotations;

namespace Domain.Entity;

public class Notification
{
    [Key]
    public int Id { get; set; }
    public string? Title { get; set; }
    public string Message { get; set; }
    public DateTime Time { get; set; }
    public bool IsRead { get; set; }
    public string? Photo { get; set; }
    public string? SourceUser { get; set; }
    public string DestUser { get; set; }
    public byte? Type { get; set; }
}
