namespace Application.DTO.Response;
public record UserProfile
{
    public  string FirstName { get; set; }
    public  string LastName { get; set; }
    public  string Country { get; set; }
    public  string City { get; set; }
    public  string Email { get; set; }
    public  string UserName { get; set; }
    public  int Following { get; set; }
    public  int Followers { get; set; }
    public  DateOnly DateOfBirth { get; set; }
    public string? PhotoURL { get; set; }
    public bool? IsFollowing { get; set; }
    public  IEnumerable<ReadPost>? UserPosts { get; set; }
}
