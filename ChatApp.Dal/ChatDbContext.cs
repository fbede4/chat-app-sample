using ChatApp.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Dal
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(user => user.MessagesSent)
                .WithOne(message => message.SenderUser)
                .HasForeignKey(message => message.SenderUserId);

            modelBuilder.Entity<User>()
                .HasMany(user => user.MessagesRecieved)
                .WithOne(message => message.RecipientUser)
                .HasForeignKey(message => message.RecipientUserId);
        }
    }
}
