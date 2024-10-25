﻿using Application.IServices;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Command.UploadProfilePhoto;

internal class UploadProfilePhotoCommandHandler : IRequestHandler<UploadProfilePhotoCommand, bool>
{
    private readonly UserManager<AppUser> _userManger;
    private readonly IPhotoService _photoService;

    public UploadProfilePhotoCommandHandler(UserManager<AppUser> userManager, IPhotoService photoService)
    {
        _userManger = userManager;
        _photoService = photoService;
    }
    public async Task<bool> Handle(UploadProfilePhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManger.FindByEmailAsync(request.email);

        if (user is null) return false;

        var result = await _photoService.UploadProfilePhotoAsync(user.UserName, request.photo);

        if (result.Error is not null) return false;

        user.PhotoURL = result.SecureUrl.AbsoluteUri;
        await _userManger.UpdateAsync(user);
        return true;
    }
}