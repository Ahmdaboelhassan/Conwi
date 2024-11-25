namespace Application.DTO.Response;
public class ReadPost
{
    public int id { get; set; }
    public string content { get; set; }
    public string imgUrl { get; set; }
    public string username { get; set; }
    public string userEmail { get; set; }
    public string userId { get; set; }
    public string userPhoto { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public int likes { get; set; }
    public bool isLiked { get; set; }
    public DateTime time { get; set; }
}
