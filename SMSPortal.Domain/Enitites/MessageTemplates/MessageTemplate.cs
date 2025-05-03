using SMSPortal.Domain.Enitites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Domain.Enitites.MessageTemplates
{
    public class MessageTemplate
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        

        public string? CreatedByUserId { get; set; }
       
        public ApplicationUser CreatedByUser { get; set; }  
    }
}
