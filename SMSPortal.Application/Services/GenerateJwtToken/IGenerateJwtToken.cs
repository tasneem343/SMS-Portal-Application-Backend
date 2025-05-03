using SMSPortal.Domain.Enitites.User;

namespace SMSPortal.Application.Services.GenerateJwtToken
{
    public interface IGenerateJwtToken
    {
        public Task<string> GenerateJwtTokenAsync(ApplicationUser user);

    }
}
