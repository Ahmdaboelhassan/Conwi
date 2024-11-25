namespace Domain.Entity;

public class UserLike
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int PostId { get; set; }
    public AppUser User { get; set; }
    public Post Post { get; set; }

}
