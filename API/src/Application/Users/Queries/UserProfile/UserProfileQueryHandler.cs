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
        
        // Get profile
        var userProfile =  _mapper.Map<UserProfile>(user);

        var userFollow = await _unitOfWork.UserFollow.GetAll(uf => uf.SourceUserId == user.Id || uf.DistinationUserId == user.Id);

        // Get Following and Followers
        if (request.userId != userprofileId)
            userProfile.IsFollowing = userFollow.Any(d => d.DistinationUserId == userprofileId);

        userProfile.Following = userFollow.Count(uf => uf.SourceUserId == userprofileId);
        userProfile.Followers = userFollow.Count(uf => uf.DistinationUserId == userprofileId);

        // Get Users Likes 
        var userPosts = await _unitOfWork.Posts.GetAll(x => x.UserPostedId == user.Id);
        var usersPostsIds = userPosts.Select(p => p.id);
        var userLiked = await _unitOfWork.UserLike.SelectAll(x => usersPostsIds.Any(id => id == x.PostId) &&  x.UserId == request.userId , p => p.PostId);

        userProfile.UserPosts = userPosts.Select(p => new ReadPost {
            id = p.id,
            userId = p.UserPostedId,
            userEmail = user.Email,
            username = user.UserName,
            userPhoto = user.PhotoURL,
            content = p.content,
            imgUrl = p.photoURL,
            time = p.timePosted,
            firstName = user.FirstName,
            lastName = user.LastName,
            likes = p.Likes,
            isLiked = userLiked.Any(pId => pId == p.id)

        })
        .OrderByDescending(p => p.time)
        .AsEnumerable();
        
        return userProfile;
        }
}
