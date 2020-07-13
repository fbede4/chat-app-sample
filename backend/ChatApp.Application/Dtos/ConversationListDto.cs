namespace ChatApp.Application.Dtos
{
    public class ConversationListDto
    {
        public int Id { get; set; }
        public string PartnerUserName { get; set; }
        public string LastMessage { get; set; }
    }
}
