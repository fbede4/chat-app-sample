using ChatApp.Domain.Model;
using ChatApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Dal.Repositories
{
    public class ConversationRepository : RepositoryBase<Conversation>, IConversationRepository
    {
        public ConversationRepository(ChatDbContext chatDbContext) : base(chatDbContext)
        {
        }

        public Task<Conversation> GetConversation(int conversationId)
        {
            return chatDbContext.Conversations
                .Include(c => c.FirstParticipantUser)
                .Include(c => c.SecondParticipantUser)
                .Include(c => c.Messages)
                .SingleAsync(c => c.Id == conversationId);
        }

        public Task<List<Conversation>> GetConversations(int userId)
        {
            return chatDbContext.Conversations
                .Include(c => c.FirstParticipantUser)
                .Include(c => c.SecondParticipantUser)
                .Include(c => c.Messages)
                .Where(c => c.FirstParticipantUserId == userId || c.SecondParticipantUserId == userId)
                .ToListAsync();
        }

        public Task<bool> GetIfExists(int firstUserId, int secondUserId)
        {
            return chatDbContext.Conversations
                .Where(c => (c.FirstParticipantUserId == firstUserId && c.SecondParticipantUserId == secondUserId)
                        || (c.SecondParticipantUserId == firstUserId && c.FirstParticipantUserId == secondUserId))
                .AnyAsync();
        }
    }
}
