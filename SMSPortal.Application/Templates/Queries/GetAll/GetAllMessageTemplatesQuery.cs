using MediatR;
using SMSPortal.Application.Templates.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Application.Templates.Queries.GetAll
{
    public class GetAllMessageTemplatesQuery : IRequest<List<GetAllMessageTemplatesDto>>
    {
    }
}
