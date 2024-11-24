using Application.Helper;
using Application.IServices;
using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services;

internal class TokenService : ITokenService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly JWT _Jwt;

    public TokenService(UserManager<AppUser> userManager, IOptions<JWT> jwt)
    {
        _userManager = userManager;
        _Jwt = jwt.Value;
    }

    public async Task<JwtSecurityToken> CreateToken(AppUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);

        var claims = new[]{
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())
            }
        .Union(userClaims);

        var securitKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Jwt.Key));

        var signingCredentials = new SigningCredentials(securitKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurtyToken = new JwtSecurityToken(
        issuer: _Jwt.Issuer,
            audience: _Jwt.Audience,
            signingCredentials: signingCredentials,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(_Jwt.DurationInDays)
        );

        return jwtSecurtyToken;
    }

    public Task<string> RefreshToken()
    {
        throw new NotImplementedException();
    }
}
