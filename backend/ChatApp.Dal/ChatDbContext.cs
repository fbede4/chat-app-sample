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
        public DbSet<Conversation> Conversations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Conversation>()
                .HasOne(c => c.FirstParticipantUser)
                .WithMany()
                .HasForeignKey(c => c.FirstParticipantUserId);

            modelBuilder.Entity<Conversation>()
                .HasOne(c => c.SecondParticipantUser)
                .WithMany()
                .HasForeignKey(c => c.SecondParticipantUserId);

            modelBuilder.Entity<Conversation>()
                .HasMany(c => c.Messages)
                .WithOne(u => u.Conversation)
                .HasForeignKey(c => c.ConversationId);

            modelBuilder.Entity<Message>()
                .HasOne(user => user.SentByUser)
                .WithMany()
                .HasForeignKey(user => user.SentByUserId);
        }
    }
}
