using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSPortal.Domain.Enitites.MessageTemplates;
using SMSPortal.Domain.Enitites.SentMessages;
using SMSPortal.Domain.Enitites.Logs;
using Microsoft.AspNetCore.Identity;

namespace SMSPortal.Domain.Enitites.User
{
    public class ApplicationUser:IdentityUser
    {
        public string? FullName { get; set; }

        public ICollection<SentMessage> SentMessages { get; set; }  
        public ICollection<MessageTemplate> MessageTemplates { get; set; }
        public ICollection<Log> Logs { get; set; }  

    }

}
