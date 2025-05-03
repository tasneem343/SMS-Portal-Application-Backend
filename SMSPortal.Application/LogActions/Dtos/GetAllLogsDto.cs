using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Application.LogActions.Dtos
{
    public class GetAllLogsDto
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string PerformedByUserId { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Details { get; set; }
        public string UserName { get; set; }
    }
}
