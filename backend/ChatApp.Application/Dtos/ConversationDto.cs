using System.Collections.Generic;

namespace ChatApp.Application.Dtos
{
    public class ConversationDto
    {
        public int Id { get; set; }
        public string PartnerUserName { get; set; }
        public List<MessageDto> Messages { get; set; }
    }
}
