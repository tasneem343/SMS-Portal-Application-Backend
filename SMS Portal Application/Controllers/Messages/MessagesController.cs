using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMSPortal.Application.Interfaces.SendMessage.Commands;
using SMSPortal.Application.SendBulkSms.Command;
using Twilio.Rest.Api.V2010.Account;

namespace SMS_Portal_Application.Controllers.Messages
{
    [Route("[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("send")]
        public async Task<IActionResult> Send([FromForm]SendMessageCommand command)
        {
            var result = await _mediator.Send(command);
            if (!string.IsNullOrEmpty(result.ErrorMessage))
                return BadRequest(result.ErrorMessage);

            return Ok(result);
        }
        [HttpPost("send-bulk")]
        public async Task<ActionResult<List<MessageResource>>> SendBulk([FromForm] SendBulkSmsCommand command)
        {
            var results = await _mediator.Send(command);
            return Ok(results);
        }
    }
}
