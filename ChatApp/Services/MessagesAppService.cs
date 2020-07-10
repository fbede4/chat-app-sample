using System.Collections.Generic;

namespace ChatApp.Services
{
    public class MessagesAppService
    {
        private readonly List<string> messages;

        public MessagesAppService()
        {
            messages = new List<string>
            {
                "Szia",
                "Szia",
                "Mi újság?",
                "semmi"
            };
        }

        public List<string> GetMessages()
        {
            return messages;
        }
    }
}
