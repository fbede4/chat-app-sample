using ChatApp.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Application.Interfaces
{
    public interface IConversationsAppService
    {
        Task<List<ConversationListDto>> GetConversations(int userId);
        Task<ConversationDto> GetConversation(int conversationId, int userId);
        Task<int> CreateConversation(ConversationCreateDto dto);
    }
}
