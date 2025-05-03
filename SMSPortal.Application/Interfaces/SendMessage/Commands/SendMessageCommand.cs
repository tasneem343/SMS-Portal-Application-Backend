using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Twilio.Rest.Api.V2010.Account;

namespace SMSPortal.Application.Interfaces.SendMessage.Commands
{
    public class SendMessageCommand:IRequest<MessageResource>
    {
        public string PhoneNumber { get; set; }
        public string MessageContent { get; set; }
        public string SenderUserId { get; set; }
    }
}
