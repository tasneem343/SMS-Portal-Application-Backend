using SMSPortal.Domain.Enitites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Domain.Enitites.Logs
{
    public class Log
    {

        public int Id { get; set; }
        public string Action { get; set; } 
        public string? PerformedByUserId { get; set; } 
        public DateTime Timestamp { get; set; }  
        public string? Details { get; set; }  

        public ApplicationUser PerformedByUser { get; set; } 
    }
}
