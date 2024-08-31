using Application.DTO.Response;
using AutoMapper;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Queries.UserProfileQuery;

public class UserProfileQueryHandler : IRequestHandler<UserProfileQuery, UserProfile?>
{
    private readonly UserManager<AppUser> _userManger;
    private readonly IMapper _mapper;

    public UserProfileQueryHandler(UserManager<AppUser> userManger, IMapper mapper)
    {
        _userManger = userManger;
        _mapper = mapper;
    }

    public async Task<UserProfile?> Handle(UserProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManger.FindByEmailAsync(request.userEmail);

        if (user is null)
            return null;

        return _mapper.Map<UserProfile>(user);
    }
}
