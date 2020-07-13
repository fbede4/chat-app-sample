using ChatApp.Application.Dtos;
using ChatApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Api.Controllers
{
    [Route("conversations")]
    public class ConversationsController : ControllerBase
    {
        private readonly IConversationsAppService conversationsAppService;

        public ConversationsController(
            IConversationsAppService conversationsAppService)
        {
            this.conversationsAppService = conversationsAppService;
        }

        [HttpGet]
        public Task<List<ConversationListDto>> GetConversations([FromQuery]int userId)
        {
            return conversationsAppService.GetConversations(userId);
        }

        [HttpGet("{conversationId}")]
        public Task<ConversationDto> GetConversation(int conversationId, [FromQuery]int userId)
        {
            return conversationsAppService.GetConversation(conversationId, userId);
        }

        [HttpPost]
        public Task<int> CreateConversation(int firstUserId, int secondUserId)
        {
            return conversationsAppService.CreateConversation(new ConversationCreateDto
            {
                FirstUserId = firstUserId,
                SecondUserId = secondUserId
            });
        }
    }
}
