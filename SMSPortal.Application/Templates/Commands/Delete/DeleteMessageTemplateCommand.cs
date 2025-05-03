using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Application.Templates.Commands.Delete
{
    public class DeleteMessageTemplateCommand:IRequest<bool>
    {
        public int Id { get; set; }
        public string DeletedBy { get; set; }

    }
}
