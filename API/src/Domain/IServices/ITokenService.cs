using Domain.Entity;
using System.IdentityModel.Tokens.Jwt;

namespace Domain.IServices;

public interface ITokenService
{
    Task<JwtSecurityToken> CreateToken(AppUser user);
    Task<string> RefreshToken();

}
