using SMSPortal.Domain.Enitites.SentMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Application.Interfaces.Repositories.SentMessages
{
    public interface ISentMessageRepo
    {

        Task<IEnumerable<SentMessage>> GetAllAsync();
        Task AddAsync(SentMessage message);
    }
}
