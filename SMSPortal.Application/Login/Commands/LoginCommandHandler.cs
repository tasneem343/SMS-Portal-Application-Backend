

using MediatR;
using Microsoft.AspNetCore.Http;
using SMSPortal.Application.Auth.Login.Response;
using SMSPortal.Application.Interfaces.Repositories.Logs;
using SMSPortal.Application.Services.Login;
using System.Security.Claims;

namespace SMSPortal.Application.Auth.Login.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    { private readonly ILoginService loginService;
        public LoginCommandHandler(ILoginService _loginService,ILogRepo logRepo)
        {
            loginService = _loginService;


        }
       
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var token = await loginService.Login(request.Email, request.Password);
            if (token != null)
            {
               
                return new LoginResponse
                {
                    Token = token,
                    Success = true,
                    Id= await loginService.GetUserId(request.Email)
                };
                
            }
            else
            {
                return new LoginResponse
                {
                    Token = null,
                    Success = false,
                    Id = null
                };
            }
        }
    }
}
