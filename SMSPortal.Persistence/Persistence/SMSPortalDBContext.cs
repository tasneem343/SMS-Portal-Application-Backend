using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SMSPortal.Domain.Enitites.Logs;
using SMSPortal.Domain.Enitites.MessageTemplates;
using SMSPortal.Domain.Enitites.SentMessages;
using SMSPortal.Domain.Enitites.User;

namespace SMSPortal.Persistence.Persistence
{
    public class SMSPortalDBContext : IdentityDbContext<ApplicationUser> 
    {
        public SMSPortalDBContext(DbContextOptions<SMSPortalDBContext> options) : base(options) { }

        public DbSet<MessageTemplate> MessageTemplate { get; set; }
        public DbSet<SentMessage> SentMessages { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // MessageTemplate Config
            builder.Entity<MessageTemplate>()
                .HasOne<ApplicationUser>(m => m.CreatedByUser)
                .WithMany(u => u.MessageTemplates)
                .HasForeignKey(m => m.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // SentMessage Config
            builder.Entity<SentMessage>()
                .HasOne<ApplicationUser>(s => s.SenderUser)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(s => s.SenderUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Log Config
            builder.Entity<Log>()
                .HasOne<ApplicationUser>(l => l.PerformedByUser)
                .WithMany(u => u.Logs)
                .HasForeignKey(l => l.PerformedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
