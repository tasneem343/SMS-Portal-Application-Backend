using SMSPortal.Domain.Enitites.Logs;


namespace SMSPortal.Application.Interfaces.Repositories.Logs
{
    public interface ILogRepo
    {
        Task<IEnumerable<Log>> GetAllAsync();
        Task AddAsync(string action, string performedByUserId, string? details = null);
    }
}