using System;

namespace ChatApp.Domain.Model
{
    public class Message
    {
        public int Id { get; set; }
        
        public DateTime CreateDate { get; set; }
        public string Text { get; set; }

        public int SenderUserId { get; set; }
        public virtual User SenderUser { get; set; }

        public int RecipientUserId { get; set; }
        public virtual User RecipientUser { get; set; }
    }
}
