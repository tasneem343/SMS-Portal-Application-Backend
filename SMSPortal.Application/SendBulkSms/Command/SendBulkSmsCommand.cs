using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace SMSPortal.Application.SendBulkSms.Command
{
   public class SendBulkSmsCommand : IRequest<List<int>>
    {
        public IFormFile CsvFile { get; set; }
        public string MessageContent { get; set; }
        public string SenderUserId { get; set; }
    }
}
    

