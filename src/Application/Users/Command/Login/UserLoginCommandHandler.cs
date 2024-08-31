using Application.DTO.Response;
using Application.IServices;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Users.Command.Login
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, AuthResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public UserLoginCommandHandler(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public  async Task<AuthResponse> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var model = request.model;
            // [1] checking the email and password
            var userWithSameUsername = await _userManager.FindByNameAsync(model.Email);
            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);

            if (userWithSameEmail is null && userWithSameUsername is null)
                return new AuthResponse { Messages = new List<string> { "Invalid Email Or Username !!" } };

            var userFromDb = new AppUser();

            if (userWithSameEmail is null)
                userFromDb = userWithSameUsername;

            if (userWithSameUsername is null)
                userFromDb = userWithSameEmail;

            //[2] check for password
            var isPasswordTrue = await _userManager.CheckPasswordAsync(userFromDb, model.Password);
            if (!isPasswordTrue)
                return new AuthResponse { Messages = new List<string> { "Password is not correct" } };

            var tokenInfo = await _tokenService.CreateToken(userFromDb);
            var tokenExpiresOn = tokenInfo.ValidTo;
            var token = new JwtSecurityTokenHandler().WriteToken(tokenInfo);

            // check if is email has not confrimed
            var isConfrimed = await _userManager.IsEmailConfirmedAsync(userFromDb);

            return new AuthResponse
            {
                Messages = new List<string> { "You are signing in successfully" },
                IsAuthenticated = true,
                ExpireOn = tokenExpiresOn,
                Token = token,
                Email = userFromDb.Email,
                UserName = userFromDb.UserName,
                IsEmailConfrimed = isConfrimed
            };
        }
    }
}
