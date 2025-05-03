using MediatR;
using SMSPortal.Application.LogActions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Application.LogActions.Queries
{
    public class GetAllLogsQuery:IRequest<List<GetAllLogsDto>>
    {
    }
}
