using System.Collections.Generic;

namespace ChatApp.Domain.Model
{
    public class Conversation
    {
        public int Id { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public int FirstParticipantUserId { get; set; }
        public virtual User FirstParticipantUser { get; set; }

        public int SecondParticipantUserId { get; set; }
        public virtual User SecondParticipantUser { get; set; }
    }
}
