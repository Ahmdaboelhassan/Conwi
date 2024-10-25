using Application.DTO.Request;
using Application.Users.Command.AddPost;
using Application.Users.Command.UploadProfilePhoto;
using Application.Users.Queries.GetFollowingPosts;
using Application.Users.Queries.GetUserPosts;
using Application.Users.Queries.GetUsers;
using Application.Users.Queries.UserProfileQuery;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace API.Controllers;

public class UserController : BaseController
{   
    private readonly IMediator _mediator;
    private readonly IStringLocalizer<UserController> _localizer;
    public UserController(IMediator mediator , IStringLocalizer<UserController> localizer)
    {
        _mediator = mediator;
        _localizer = localizer;
    }

    [HttpGet]
    [Route("UserProfile/{email}")]
    public async Task<IActionResult> UserProfile(string email){

        return Ok(await _mediator.Send(new UserProfileQuery(email)));
    }

    [HttpGet]
    [Route("UserPosts/{userId:guid}")]
    public async Task<IActionResult> UserPosts(string userId){

        return Ok(await _mediator.Send(new GetUserPostsQuery(userId)));
    }

    [HttpGet]
    [Route("FollowingPosts/{userId:guid}")]
    public async Task<IActionResult> FollowingPosts(string userId){

        return Ok(await _mediator.Send(new GetFollowingPostsQuery(userId)));
    }

    [HttpGet]
    [Route("GetUsers")]
    public async Task<IActionResult> GetUsers(string criteria){

        return Ok(await _mediator.Send(new GetUsersQuery(criteria)));
    }

    [HttpPost]
    [Route("CreatePost")]
    public async Task<IActionResult> CreatePost([FromForm] CreatePost newPost){

        var success = await _mediator.Send(new CreatePostCommand(newPost));

        if (!success)
            return BadRequest(_localizer["CreatePostFailed"].Value);

        return Ok();
    }

    [HttpPost]
    [Route("UploadProfilePhoto")]
    public async Task<IActionResult> UploadProfilePhoto(string email , IFormFile photo){

        var success = await _mediator.Send(new UploadProfilePhotoCommand(email, photo));

        if (!success)
            return BadRequest(_localizer["UploadPhotoFailed"].Value);

        return Ok();
    }
}
