using ChatApp.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Domain.Repositories
{
    public interface IConversationRepository : IRepository<Conversation>
    {
        Task<List<Conversation>> GetConversations(int userId);
        Task<Conversation> GetConversation(int conversationId);
    }
}
