using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Message> MessagesSent { get; set; }
        public virtual ICollection<Message> MessagesRecieved { get; set; }
    }
}
