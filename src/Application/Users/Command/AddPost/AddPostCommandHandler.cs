using Application.IRepository;
using Application.IServices;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Command.AddPost;

public class AddPostCommandHandler :  IRequestHandler<AddPostCommand, bool>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IPhotoService _photoService;
    private readonly IUnitOfWork _unitOfWork;

    public AddPostCommandHandler(UserManager<AppUser> userManager, IPhotoService photoService, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _photoService = photoService;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(AddPostCommand request, CancellationToken cancellationToken)
    {
        var model = request.PostModel;
        if (model.content.Length == 0) return false;

        var user = await _userManager.FindByEmailAsync(model.email);
        if (user is null) return false;

        var result = await _photoService.UploadPostPhotoAsync(user.UserName, model.photo);

        if (result.Error is not null) return false;

        var newPost = new Post
        {
            content = model.content,
            timePosted = DateTime.UtcNow,
            UserPostedId = user.Id,
            photoURL = result.SecureUrl.AbsoluteUri
        };

        await _unitOfWork.Posts.AddAsync(newPost);

        if (_unitOfWork.SaveChanges() == 0)
            return false;

        return true;
    }
}


