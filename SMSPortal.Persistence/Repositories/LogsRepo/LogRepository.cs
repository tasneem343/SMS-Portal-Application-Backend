using Microsoft.EntityFrameworkCore;
using SMSPortal.Application.Interfaces.Repositories.Logs;
using SMSPortal.Domain.Enitites.Logs;
using SMSPortal.Persistence.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Persistence.Repositories.Logs
{
    public class LogRepository : ILogRepo
    {
        private readonly SMSPortalDBContext _dbContext;
        public LogRepository(SMSPortalDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(string action, string performedByUserId, string? details = null)
        {
            var log = new Log
            {
                Action = action,
                PerformedByUserId = performedByUserId,
                Timestamp = DateTime.Now,
                Details = details
            };
            await _dbContext.Logs.AddAsync(log);

            await _dbContext.SaveChangesAsync();
        }

     

        public async Task<IEnumerable<Log>> GetAllAsync()
        {
            var logs = await _dbContext.Logs.Include(x=>x.PerformedByUser).ToListAsync();
            return logs;
        }
    }
}
