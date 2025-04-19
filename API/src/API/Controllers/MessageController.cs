using Application.DTO.Request;
using Application.Messages.Command.DeleteMessage;
using Application.Messages.Command.SendMessage;
using Application.Messages.Queries.GetAllChats;
using Application.Messages.Queries.GetPrivateChat;
using Application.Messages.Queries.GetUnreadMessages;
using Application.Users.Extension;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Authorize]
public class MessageController : BaseController
{
    private readonly IMediator _mediator;
    public MessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("GetAllChats/{userId:guid}")]
    public async Task<IActionResult> GetAllChats(string userId)
    {
        return Ok(await _mediator.Send(new GetAllChatQuery(userId)));
    }

    [HttpGet]
    [Route("GetPrivateChat/{user:guid}/{contact:guid}")]
    public async Task<IActionResult> GetPrivateChat(string user, string contact)
    {
        return Ok(await _mediator.Send(new GetPrivateChatQuery(user , contact)));
    }

    [HttpGet]
    [Route("GetUnReadMessages")]
    public async Task<IActionResult> GetInReadMessages()
    {
        return Ok(await _mediator.Send(new GetUnReadMessageQuery()));
    }
    [HttpDelete]
    [Route("DeleteMessage/{id:int}")]
    public async Task<IActionResult> DeleteMessage(int messageId, string userId)
    {
        if (await _mediator.Send(new DeleteMessageCommand(messageId , userId)))
            return Ok();

        return BadRequest("Error Happened");
    }

    [HttpPost]
    [Route("SendMessage")]
    public async Task<IActionResult> SendMessage(SendMessage DTO)
    {
        if( await _mediator.Send(new SendMessageCommand(DTO)))
            return Ok();

        return BadRequest("Error Happened");
    }

}

