using ChatApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{
    [Route("messages")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesAppService messagesAppService;

        public MessagesController(
            IMessagesAppService messagesAppService)
        {
            this.messagesAppService = messagesAppService;
        }

        [HttpPost]
        public Task CreateMessage(string message, int sentByUserId, int conversationId)
        {
            return messagesAppService.SendMessage(message, sentByUserId, conversationId);
        }
    }
}
