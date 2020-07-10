using ChatApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ChatApp.Controllers
{
    [Route("messages")]
    public class MessagesController : ControllerBase
    {
        private readonly MessagesAppService messagesAppService;

        public MessagesController(
            MessagesAppService messagesAppService)
        {
            this.messagesAppService = messagesAppService;
        }

        [HttpGet]
        public List<string> GetMessages()
        {
            return messagesAppService.GetMessages();
        }
    }
}
