using System.Security.Claims;

namespace Application.Users.Extension;

public static class UserExtensions
{
    public static string? GetUserId(this ClaimsPrincipal user)
    {
        return user?.FindFirstValue(ClaimTypes.NameIdentifier);
    }

}
