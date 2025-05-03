using MediatR;
using SMSPortal.Application.Auth.Login.Response;

namespace SMSPortal.Application.Auth.Login.Commands
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
