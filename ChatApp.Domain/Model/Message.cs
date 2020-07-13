using System;

namespace ChatApp.Domain.Model
{
    public class Message
    {
        public int Id { get; set; }
        
        public DateTime CreateDate { get; set; }
        public string Text { get; set; }

        public int ConversationId { get; set; }
        public virtual Conversation Conversation { get; set; }

        public int SentByUserId { get; set; }
        public virtual User SentByUser { get; set; }
    }
}
