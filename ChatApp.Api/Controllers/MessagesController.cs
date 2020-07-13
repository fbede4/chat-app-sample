using ChatApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpGet("recieved")]
        public Task<List<string>> GetRecievedMessages(int userId)
        {
            return messagesAppService.GetRecievedMessages(userId);
        }

        [HttpPost]
        public Task CreateMessage(string message, int senderUserId, int recipientUserId)
        {
            return messagesAppService.CreateMessage(message, senderUserId, recipientUserId);
        }
    }
}
