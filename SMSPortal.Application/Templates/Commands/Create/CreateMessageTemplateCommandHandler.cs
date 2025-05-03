using MediatR;
using SMSPortal.Application.Interfaces.Repositories.Logs;
using SMSPortal.Application.Interfaces.Repositories.MessageTemplates;
using SMSPortal.Domain.Enitites.MessageTemplates;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SMSPortal.Application.Templates.Commands.Create
{
    internal class CreateMessageTemplateCommandHandler : IRequestHandler<CreateMessageTemplateCommand, bool>
    {
        private readonly IMessageTemplateRepo _messageTemplateRepo;
        private readonly ILogRepo _logRepo;
        public CreateMessageTemplateCommandHandler(IMessageTemplateRepo messageTemplateRepo,ILogRepo logRepo)
        {
            _logRepo = logRepo;
            _messageTemplateRepo = messageTemplateRepo;
        }

        public async Task<bool> Handle(CreateMessageTemplateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var template = new MessageTemplate
                {
                    Title = request.Title,
                    Content = request.Content,
                    CreatedByUserId = request.CreatedByUserId
                };

                await _messageTemplateRepo.AddAsync(template);
                await _logRepo.AddAsync("message Template Created",request.CreatedByUserId,$"Title: {request.Title}");
                return true;  
            }
            catch (Exception)
            {
                return false; 
            }
        }
    }
}
