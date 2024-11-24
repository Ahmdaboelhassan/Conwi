using Application.DTO.Response;
using Application.IRepository;
using AutoMapper;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Queries.UserProfileQuery;

public class UserProfileQueryHandler : IRequestHandler<UserProfileQuery, UserProfile?>
{
    private readonly UserManager<AppUser> _userManger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserProfileQueryHandler(UserManager<AppUser> userManger, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _userManger = userManger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserProfile?> Handle(UserProfileQuery request, CancellationToken cancellationToken)
    {
        var userprofileId = request.id;
        var user = await _userManger.FindByIdAsync(userprofileId);

        if (user is null) return null;
        
        var userProfile =  _mapper.Map<UserProfile>(user);

        var userFollow = await _unitOfWork.UserFollow.GetAll(uf => uf.SourceUserId == user.Id || uf.DistinationUserId == user.Id);

        if (request.userId != userprofileId)
            userProfile.IsFollowing = userFollow.Any(d => d.DistinationUserId == userprofileId);

        userProfile.Following = userFollow.Count(uf => uf.SourceUserId == userprofileId);
        userProfile.Followers = userFollow.Count(uf => uf.DistinationUserId == userprofileId);


        var userPosts = await _unitOfWork.Posts.GetAll(x => x.UserPostedId == user.Id);
        userProfile.UserPosts = userPosts.Select(p => new ReadPost {
             Id = p.id,
             content = p.content,
             imgUrl = p.photoURL,
             userEmail = user.Email,
             userId = user.Id,
             username = user.UserName,
             userPhoto = user.PhotoURL,
             Time = p.timePosted
        })
        .OrderByDescending(p => p.Time)
        .AsEnumerable();
        
        return userProfile;
    }
}
