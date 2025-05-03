using SMSPortal.Domain.Enitites.MessageTemplates;
using SMSPortal.Domain.Enitites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Domain.Enitites.SentMessages
{
    public class SentMessage
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string MessageContent { get; set; }
        public DateTime SentAt { get; set; }
        public bool? IsSuccessful { get; set; }

        public string SenderUserId { get; set; }
        public ApplicationUser SenderUser { get; set; }

        public int? TemplateId { get; set; }
        public MessageTemplate Template { get; set; }  
    }
}
