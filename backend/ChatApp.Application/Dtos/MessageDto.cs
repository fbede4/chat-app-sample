using System;

namespace ChatApp.Application.Dtos
{
    public class MessageDto
    {
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool IsSent { get; set; }
    }
}
