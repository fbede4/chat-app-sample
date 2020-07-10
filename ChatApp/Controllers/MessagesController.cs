using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ChatApp.Controllers
{
    [Route("messages")]
    public class MessagesController : ControllerBase
    {
        private readonly List<string> messages;

        public MessagesController()
        {
            messages = new List<string>
            {
                "Szia",
                "Szia",
                "Mi újság?",
                "semmi"
            };
        }

        [HttpGet]
        public List<string> GetMessages()
        {
            return messages;
        }
    }
}
