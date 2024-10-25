using Application.DTO.Request;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace Application.Users.Queries.GetEmailConfirmationToken
{
    public class EmailConfirmationTokenQueryHandler : IRequestHandler<EmailConfirmationTokenQuery, ConfirmationResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        public EmailConfirmationTokenQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ConfirmationResponse> Handle(EmailConfirmationTokenQuery request, CancellationToken cancellationToken)
        {
            var userFromDb = await _userManager.FindByEmailAsync(request.email);
            if (userFromDb is null)
                return new ConfirmationResponse { Messages = "User Not Found" };

            string? confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(userFromDb);
            if (confirmationToken is null)
                return new ConfirmationResponse { Messages = "An Error Occured" };

            return new ConfirmationResponse
            {
                isGenerated = true,
                Token = confirmationToken,
                Messages = "Email Comfirmation Token Has Been Generated Successfully",
            };
        }
    }
}
