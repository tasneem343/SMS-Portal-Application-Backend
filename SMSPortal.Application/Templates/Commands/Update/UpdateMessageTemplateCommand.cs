using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Application.Templates.Commands.Update
{
    public class UpdateMessageTemplateCommand:IRequest<bool>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UpdatedBy { get; set; }
    }
}
