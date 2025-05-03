using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Application.Templates.Commands.Create
{
    public class CreateMessageTemplateCommand : IRequest<bool>
    {

        public string Title { get; set; }
        public string Content { get; set; }
        public string? CreatedByUserId { get; set; }

    }
}
