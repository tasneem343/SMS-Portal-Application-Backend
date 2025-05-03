using MediatR;
using Microsoft.AspNetCore.Http;
using SMSPortal.Application.Interfaces.Repositories.Logs;
using SMSPortal.Application.Interfaces.Repositories.MessageTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Application.Templates.Commands.Update
{
    public class UpdateMessageTemplateCommandHandler : IRequestHandler<UpdateMessageTemplateCommand, bool>
    {
        
            private readonly IMessageTemplateRepo _messageTemplateRepo;
        private readonly ILogRepo _logrepo;
        public UpdateMessageTemplateCommandHandler(IMessageTemplateRepo messageTemplateRepo,  ILogRepo logrepo)
        {
            _messageTemplateRepo = messageTemplateRepo;
            _logrepo = logrepo;

        }

        public async Task<bool> Handle(UpdateMessageTemplateCommand request, CancellationToken cancellationToken)
        {
            var template = await _messageTemplateRepo.GetByIdAsync(request.Id);

            if (template == null)
            {
                return false;
            }

            template.Title = request.Title;
            template.Content = request.Content;
            await _logrepo.AddAsync("Message Template Updated",request.UpdatedBy, $"Template With Title:{template.Title}");
            await _messageTemplateRepo.Update(template); 

            return true;
        }
    }
}
    


