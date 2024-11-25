using Application.DTO.Request;
using Application.Post.Commands.CreatePost;
using Application.Post.Commands.DeletePost;
using Application.Post.Commands.LikePost;
using Application.Post.Queries.GetFollowingPosts;
using Application.Post.Queries.GetUserPosts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace API.Controllers
{
    [Authorize]
    public class PostController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<UserController> _localizer;
        public PostController(IMediator mediator, IStringLocalizer<UserController> localizer)
        {
            _mediator = mediator;
            _localizer = localizer;
        }

        [HttpGet]
        [Route("UserPosts/{userId:guid}")]
        public async Task<IActionResult> UserPosts(string userId)
             =>  Ok(await _mediator.Send(new GetUserPostsQuery(userId)));

        [HttpGet]
        [Route("FollowingPosts/{userId:guid}")]
        public async Task<IActionResult> FollowingPosts(string userId)
           => Ok(await _mediator.Send(new GetFollowingPostsQuery(userId)));
        

        [HttpPost]
        [Route("CreatePost")]
        public async Task<IActionResult> CreatePost([FromForm] CreatePost newPost)
        {
            var success = await _mediator.Send(new CreatePostCommand(newPost));

            return !success ? BadRequest(_localizer["CreatePostFailed"].Value): Ok();
        }

        [HttpGet]
        [Route("DeletePost/{postId}/{userId:guid}")]
        public async Task<IActionResult> DeletePost(int postId , string userId)
        {
            var success = await _mediator.Send(new DeletePostCommand(postId , userId));

            return !success ? BadRequest(_localizer["CreatePostFailed"].Value) : Ok();
        }

        [HttpPost]
        [Route("LikePost")]
        public async Task<IActionResult> LikePost(LikePost model)
        {
            var success = await _mediator.Send(new LikePostCommand(model.postId , model.userId));

            return !success ? BadRequest(_localizer["CreatePostFailed"].Value) : Ok();
        }
    }
}
