using Application.DTO.Request;
using Application.Users.Command;
using Application.Users.Command.FollowUser;
using Application.Users.Queries.GetNonFollowingUsers;
using Application.Users.Queries.GetUsers;
using Application.Users.Queries.UserProfileQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace API.Controllers;

[Authorize]
public class UserController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IStringLocalizer<UserController> _localizer;
    public UserController(IMediator mediator, IStringLocalizer<UserController> localizer)
    {
        _mediator = mediator;
        _localizer = localizer;
    }

    [HttpGet]
    [Route("UserProfile/{id}/{userId}")]
    public async Task<IActionResult> UserProfile(string id , string userId) 
         => Ok(await _mediator.Send(new UserProfileQuery(id ,userId)));
    

    [HttpGet]
    [Route("SearchUsers")]
    public async Task<IActionResult> SearchUsers(string? criteria , string userId) 
        => Ok(await _mediator.Send(new GetUsersQuery(criteria , userId)));
    

    [HttpGet]
    [Route("ExploreUsers/{userId:guid}")]
    public async Task<IActionResult> ExploreUsers(string userId) 
        => Ok(await _mediator.Send(new ExploreUsersQuery(userId)));


    [HttpPost]
    [Route("UploadProfilePhoto")]
    public async Task<IActionResult> UploadProfilePhoto([FromForm] UploadProfilePhoto model) {

        var success = await _mediator.Send(new UploadProfilePhotoCommand(model));

        if (!success)
            return BadRequest(_localizer["UploadPhotoFailed"].Value);

        return Ok();
    }

    [HttpPost]
    [Route("FollowUser")]
    public async Task<IActionResult> FollowUser(FollowUserRequest request)
    {
        await _mediator.Send(new FollowUserCommand(request));
        return Ok();
    }
}
