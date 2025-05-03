using MediatR;
using Microsoft.AspNetCore.Http;
using SMSPortal.Application.Interfaces.Repositories.Logs;
using SMSPortal.Application.Services.Logout;
using System.Security.Claims;


namespace SMSPortal.Application.Auth.logout.Commands
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        private readonly ILogoutService _logoutService;
        public LogoutCommandHandler(ILogoutService logoutService,ILogRepo logrepo)
        {
            _logoutService = logoutService;
            

        }
        public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            if (string.IsNullOrEmpty(userId))
        {
                throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));
            }
            
            await _logoutService.Logout(userId);
        }
    }
}
