using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Command.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
    {
        private readonly UserManager<AppUser> _userManager;

        public ConfirmEmailCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByEmailAsync(request.email);

            if (user is null) return false;

            var results = await _userManager.ConfirmEmailAsync(user, request.token);

            return results.Succeeded;
        }
    }
}
