using MediatR;
using Microsoft.AspNetCore.Identity;
using SMSPortal.Application.Interfaces.Repositories.Logs;
using SMSPortal.Application.LogActions.Dtos;
using SMSPortal.Domain.Enitites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Application.LogActions.Queries
{
    public class GetAllLogsQueryHandler : IRequestHandler<GetAllLogsQuery, List<GetAllLogsDto>>
    {
        private readonly ILogRepo _logRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        public GetAllLogsQueryHandler(ILogRepo logRepo,UserManager<ApplicationUser> userManager)
        {
            _logRepo = logRepo;
            _userManager = userManager;
        }
        public async Task<List<GetAllLogsDto>> Handle(GetAllLogsQuery request, CancellationToken cancellationToken)
        {
            var logs = await _logRepo.GetAllAsync();

            var logsDtos = new List<GetAllLogsDto>();

            foreach (var log in logs)
            {
                var user = await _userManager.FindByIdAsync(log.PerformedByUserId);

                logsDtos.Add(new GetAllLogsDto
                {
                    UserName = user?.UserName ?? "Unknown",
                    PerformedByUserId = log.PerformedByUserId,
                    Action = log.Action,
                    Details = log.Details,
                    Id = log.Id,
                    Timestamp = log.Timestamp,
                });
            }

            return logsDtos;
        }

    }
}
