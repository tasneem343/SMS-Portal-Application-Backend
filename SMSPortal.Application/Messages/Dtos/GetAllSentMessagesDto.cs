using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Application.Messages.Dtos
{
    public class GetAllSentMessagesDto
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string MessageContent { get; set; }
        public DateTime SentAt { get; set; }
        public string SenderUserName { get; set; }
    }
}
