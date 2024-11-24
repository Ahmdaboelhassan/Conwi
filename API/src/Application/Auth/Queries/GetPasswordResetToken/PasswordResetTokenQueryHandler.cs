using Application.DTO.Request;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Queries.GetPasswordResetToken
{
    public class PasswordResetTokenQueryHandler : IRequestHandler<PasswordResetTokenQuery, ConfirmationResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        public PasswordResetTokenQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ConfirmationResponse> Handle(PasswordResetTokenQuery request, CancellationToken cancellationToken)
        {
            var userFromDb = await _userManager.FindByEmailAsync(request.email);
            if (userFromDb is null)
                return new ConfirmationResponse { Messages = "User Not Found!!" };

            string? token = await _userManager.GeneratePasswordResetTokenAsync(userFromDb);

            if (token is null)
                return new ConfirmationResponse { Messages = "An Error Occured!!" };

            return new ConfirmationResponse
            {
                Messages = "Password Reset Token Has Been Genrated Successfully",
                isGenerated = true,
                Token = token
            }; ;
        }
    }
}
