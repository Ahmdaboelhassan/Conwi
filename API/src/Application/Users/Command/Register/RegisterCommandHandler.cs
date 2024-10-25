using Application.DTO.Response;
using Application.IServices;
using AutoMapper;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Users.Command.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public  async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            // [1] Check if is there user with the same email
            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userWithSameEmail is not null)
                return new AuthResponse { Messages = new List<string> { "This Email Is Aready Exists !!" } };

            // [2] Check if is there user with the same username
            var userWithSameUsername = await _userManager.FindByNameAsync(model.Username);
            if (userWithSameUsername is not null)
                return new AuthResponse { Messages = new List<string> { "This Username Is Aready Exists !!" } };

            // [3] register user and send token 
            var newUser = _mapper.Map<AppUser>(model);

            var results = await _userManager.CreateAsync(newUser, model.Password);

            if (!results.Succeeded)
            {
                var errorMessages = new List<string>();
                foreach (var error in results.Errors)
                {
                    errorMessages.Add(error.Description);
                }
                return new AuthResponse { Messages = errorMessages };
            }

            var tokenInfo = await _tokenService.CreateToken(newUser);
            var tokenExpiresOn = tokenInfo.ValidTo;
            var token = new JwtSecurityTokenHandler().WriteToken(tokenInfo);

            // make email confimation code
            await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

            return new AuthResponse
            {
                Id = newUser.Id,
                Messages = new List<string> { "User has been registered successfully" },
                Email = newUser.Email,
                UserName = newUser.UserName,
                Token = token,
                IsAuthenticated = true,
                ExpireOn = tokenExpiresOn,
            };
        }
    }
}
