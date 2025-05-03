
using MediatR;
using SMSPortal.Application.Interfaces.Repositories.SentMessages;
using SMSPortal.Application.Services.SmsService;
using SMSPortal.Domain.Enitites.MessageTemplates;
using SMSPortal.Domain.Enitites.SentMessages;
using Twilio.Rest.Api.V2010.Account;

namespace SMSPortal.Application.Interfaces.SendMessage.Commands
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, MessageResource>
    {
        private readonly ISmsService _smsService;
        private readonly ISentMessageRepo sentMessageRepo; 

        public SendMessageCommandHandler(ISmsService smsService, ISentMessageRepo sentMessageRepo)
        {
            _smsService = smsService;
            this.sentMessageRepo = sentMessageRepo;
        }

        public async Task<MessageResource> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
           var result = await _smsService.Send(request.PhoneNumber, request.MessageContent);

            


            var message = new SentMessage
            {
                PhoneNumber = request.PhoneNumber,
                MessageContent = request.MessageContent,
                SentAt = DateTime.UtcNow,
                SenderUserId = request.SenderUserId,
                TemplateId = null 
            };
           

            
            await sentMessageRepo.AddAsync(message);

            return result;
        }
    }
}
