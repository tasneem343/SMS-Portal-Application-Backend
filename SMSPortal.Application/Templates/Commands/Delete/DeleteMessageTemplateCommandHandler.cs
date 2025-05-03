using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SMSPortal.Application.Interfaces.Repositories.Logs;
using SMSPortal.Application.Interfaces.Repositories.MessageTemplates;
using SMSPortal.Domain.Enitites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Application.Templates.Commands.Delete
{
    internal class DeleteMessageTemplateCommandHandler:IRequestHandler<DeleteMessageTemplateCommand, bool>
    {
        private readonly IMessageTemplateRepo _messageTemplateRepo;
        private readonly ILogRepo _logrepo;

        public DeleteMessageTemplateCommandHandler(IMessageTemplateRepo messageTemplateRepo,ILogRepo logRepo)
        {
            _logrepo = logRepo;
            _messageTemplateRepo = messageTemplateRepo;
        }
        public async Task<bool> Handle(DeleteMessageTemplateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var template = await _messageTemplateRepo.GetByIdAsync(request.Id);

                if (template == null)
                {
                    return false; 
                }

                await _logrepo.AddAsync("Message Template Deleted", request.DeletedBy, $"Template With Title:{template.Title}");
                await _messageTemplateRepo.Delete(template);

                return true; 

            }
            catch (Exception)
            {
                return false; 
            }
        }
    }

}
  
