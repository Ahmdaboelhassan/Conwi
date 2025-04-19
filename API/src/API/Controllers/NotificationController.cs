using Application.Notifications.Command;
using Application.Notifications.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class NotificationController : BaseController
    {
        private readonly IMediator _mediator;
        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetUserNotifications/{userId:guid}")]
        public async Task<IActionResult> GetUserNotifications(string userId)
        {
            return Ok(await _mediator.Send(new GetUserNotificationQuery(userId)));
        }

        [HttpGet]
        [Route("GetUnReadNotification")]
        public async Task<IActionResult> GetUnReadNotification()
        {
            return Ok(await _mediator.Send(new GetUnReadNotificationQuery()));
        }

        [HttpPut]
        [Route("ReadNotification/{id:int}")]
        public async Task<IActionResult> ReadNotification(int id)
        {
            return Ok(await _mediator.Send(new ReadNotificationCommand(id)));
        }

    }
}
