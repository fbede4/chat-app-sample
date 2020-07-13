namespace ChatApp.Application.Dtos
{
    public class ConversationListDto
    {
        public int ConversationId { get; set; }
        public string PartnerUserName { get; set; }
        public string LastMessage { get; set; }
    }
}
