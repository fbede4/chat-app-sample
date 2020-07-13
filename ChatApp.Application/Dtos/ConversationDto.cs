using System.Collections.Generic;

namespace ChatApp.Application.Dtos
{
    public class ConversationDto
    {
        public string PartnerUserName { get; set; }
        public List<MessageDto> Messages { get; set; }
    }
}
