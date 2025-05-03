namespace SMSPortal.Application.Services.Login
{
    public interface ILoginService

    {
        public Task<string?> Login(string email, string password);

        public Task<string> GetUserId(string email);

    }
}
