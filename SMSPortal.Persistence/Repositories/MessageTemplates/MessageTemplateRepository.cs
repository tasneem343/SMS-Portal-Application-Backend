using Microsoft.EntityFrameworkCore;
using SMSPortal.Application.Interfaces.Repositories.MessageTemplates;
using SMSPortal.Domain.Enitites.MessageTemplates;
using SMSPortal.Persistence.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Persistence.Repositories.MessageTemplates
{
    public class MessageTemplateRepository : IMessageTemplateRepo
    {
        private readonly SMSPortalDBContext _dbcontext;
        public MessageTemplateRepository(SMSPortalDBContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task AddAsync(MessageTemplate template)
        {
          await _dbcontext.MessageTemplate.AddAsync(template);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task Delete(MessageTemplate template)
        {
            var temp = await _dbcontext.MessageTemplate.FirstOrDefaultAsync(t=>t.Id==template.Id);
           
            _dbcontext.MessageTemplate.Remove(temp);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<MessageTemplate>> GetAllAsync()
        {
            var templates = await _dbcontext.MessageTemplate.ToListAsync();
            return templates;
        }

        public async Task<MessageTemplate?> GetByIdAsync(int id)
        {
           var temp= await _dbcontext.MessageTemplate.FirstOrDefaultAsync(t => t.Id == id);
           if(temp == null)
            {
                throw new Exception("Template not found");
            }
            return temp;
        }

        public async Task Update(MessageTemplate template)
        {
           var temp = await _dbcontext.MessageTemplate.FirstOrDefaultAsync(t => t.Id == template.Id);
            if (temp != null)
            {
                temp.Title = template.Title;
                temp.Content = template.Content;
                _dbcontext.SaveChanges();
            }
            
        }
    }
}
