namespace SMSPortal.Application.Services.Logout
{
    public interface ILogoutService
    {
        Task Logout(string userId);
    }
}
