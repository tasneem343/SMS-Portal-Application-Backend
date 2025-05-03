

using MediatR;

namespace SMSPortal.Application.Auth.logout.Commands
{
    public class LogoutCommand : IRequest
    {
        public string UserId { get; set; }

    }
}
