using MediatR;
using Microsoft.AspNetCore.Mvc;
using SMSPortal.Application.Templates.Commands.Create;
using SMSPortal.Application.Templates.Commands.Delete;
using SMSPortal.Application.Templates.Commands.Update;
using SMSPortal.Application.Templates.Queries.GetAll;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Twilio.TwiML.Messaging;
using SMSPortal.Application.Interfaces.Seeder.Role;
using SMSPortal.Application.Interfaces.Seeder.User;

namespace SMSPortal.WebAPI.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class MessageTemplatesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRoleSeeder _roleSeeder;
        private readonly IUserSeeder _userSeeder;

        public MessageTemplatesController(IMediator mediator, IUserSeeder userSeeder,IRoleSeeder roleseeder)
        {
            _mediator = mediator;
            _roleSeeder = roleseeder;
            _userSeeder = userSeeder;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateMessageTemplateCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok(new { message = "Template created successfully." });
            }
            return BadRequest(new { message = "Failed to create template." });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMessageTemplateCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(new { Message = "Template ID mismatch." });
            }
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok(new { Message = "Template updated successfully." });
            }
            return BadRequest(new { Message = "Failed to update template." });
        }

        [HttpDelete("{id}by{userId}")]
        public async Task<IActionResult> Delete(int id,string userId)
        {
            
            var _command = new DeleteMessageTemplateCommand { Id = id,DeletedBy= userId};
            var result = await _mediator.Send(_command);
            if (result)
            {
                return Ok(new {Message= "Template deleted successfully." });
            }
            return BadRequest(new { Message = "Failed to delete template." });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllMessageTemplatesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("RunSeeding")]
        public async Task<IActionResult> RunSeeding()
        {
            await _roleSeeder.SeedRoles();
            await _userSeeder.SeedUsers();
            return Ok("Done");
        }



    }
}
