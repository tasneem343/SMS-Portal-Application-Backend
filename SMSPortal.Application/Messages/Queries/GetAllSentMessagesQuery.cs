using MediatR;
using SMSPortal.Application.Messages.Dtos;
using SMSPortal.Domain.Enitites.SentMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Application.Messages.Queries
{
    public class GetAllSentMessagesQuery : IRequest<List<GetAllSentMessagesDto>>
    {

    }

}
