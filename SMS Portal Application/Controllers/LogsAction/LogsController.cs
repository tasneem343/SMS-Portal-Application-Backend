using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMSPortal.Application.LogActions.Queries;
using SMSPortal.Application.Templates.Queries.GetAll;

namespace SMS_Portal_Application.Controllers.LogsAction
{
    [Route("[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LogsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLogs()
        {
            var query = new GetAllLogsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
