namespace Application.DTO.Response
{
    public record class UserProfile(
         string FirstName,
         string LastName,
         string Country,
         string City,
         string Email,
         string UserName,
         DateOnly DateOfBirth,
         string? PhotoURL
    )
    { }
}