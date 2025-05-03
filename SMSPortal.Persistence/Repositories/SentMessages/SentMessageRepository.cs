using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SMSPortal.Application.Interfaces.Repositories.SentMessages;
using SMSPortal.Domain.Enitites.SentMessages;
using SMSPortal.Persistence.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Persistence.Repositories.SentMessages
{
    public class SentMessageRepository: ISentMessageRepo
    { public SMSPortalDBContext _dbContext;
        public SentMessageRepository(SMSPortalDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(SentMessage message)
        {
           await _dbContext.SentMessages.AddAsync(message);
           await _dbContext.SaveChangesAsync();

        }
        public async Task<IEnumerable<SentMessage>> GetAllAsync()
        {
           var Messages= await _dbContext.SentMessages.Include(u=>u.SenderUser).ToListAsync();
            return Messages;

        }
    
    }
  
}
