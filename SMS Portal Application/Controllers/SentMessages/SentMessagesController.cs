using MediatR;
using Microsoft.AspNetCore.Mvc;
using SMSPortal.Application.Interfaces.Repositories.SentMessages;
using SMSPortal.Application.Messages.Queries;

namespace SMS_Portal_Application.Controllers.SentMessages
{
    [ApiController]
    [Route("[controller]")]
    public class SentMessagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SentMessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

      

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllSentMessagesQuery();
            var result = await _mediator.Send(query); 
            return Ok(result);  
        }

      
    }

}
