namespace Application.DTO.Response;

public class NotificationDTO
{   
    public int Id { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public DateTime Time { get; set; }
    public bool IsRead { get; set; }
    public string Photo { get; set; }
    public byte Type { get; set; }
}

public enum NotificationTypes
{
    Like = 1,
    Follow = 2,
}    

