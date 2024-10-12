namespace Application.DTO.Response;
public class ReadPost
{
    public int Id { get; set; }
    public string content { get; set; }
    public string imgUrl { get; set; }
    public string username { get; set; }
    public string userEmail { get; set; }
    public string userId { get; set; }
    public string userPhoto { get; set; }
    public DateTime Time { get; set; }
}
