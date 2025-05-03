using MediatR;
using SMSPortal.Application.Interfaces.Repositories.SentMessages;
using SMSPortal.Application.Messages.Dtos;
using SMSPortal.Domain.Enitites.SentMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Application.Messages.Queries
{
    public class GetAllSentMessagesHandler : IRequestHandler<GetAllSentMessagesQuery, List<GetAllSentMessagesDto>>
    {
        private readonly ISentMessageRepo _sentMessageRepo;

        public GetAllSentMessagesHandler(ISentMessageRepo sentMessageRepo)
        {
            _sentMessageRepo = sentMessageRepo;
        }

        public async Task<List<GetAllSentMessagesDto>> Handle(GetAllSentMessagesQuery request, CancellationToken cancellationToken)
        {

            var messages = await _sentMessageRepo.GetAllAsync();
            var messageDto = messages.Select(x => new GetAllSentMessagesDto
            {
                Id = x.Id,
                MessageContent = x.MessageContent,
                SenderUserName = x.SenderUser.UserName,
                SentAt = x.SentAt,
                PhoneNumber = x.PhoneNumber,
            }).ToList();
            return messageDto;
                
        }
    }

}
