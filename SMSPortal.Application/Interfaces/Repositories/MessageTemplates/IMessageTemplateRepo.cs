using SMSPortal.Domain.Enitites.MessageTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Application.Interfaces.Repositories.MessageTemplates
{
    public interface IMessageTemplateRepo
    {
        Task<IEnumerable<MessageTemplate>> GetAllAsync();
        Task<MessageTemplate?> GetByIdAsync(int id);
        Task AddAsync(MessageTemplate template);
        Task Update(MessageTemplate template);
        Task Delete(MessageTemplate template);
    }
}
