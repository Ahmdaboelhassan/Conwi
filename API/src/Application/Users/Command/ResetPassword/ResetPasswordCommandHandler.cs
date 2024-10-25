using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Command.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
{
    private readonly UserManager<AppUser> _userManager;

    public ResetPasswordCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }


    public  async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var userFromDb = await _userManager.FindByEmailAsync(request.email);
        if (userFromDb is null) return false;

        var result = await _userManager.ResetPasswordAsync(userFromDb, request.token, request.newPassword);

        return result.Succeeded;
    }
}
