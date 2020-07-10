using ChatApp.Dal;
using ChatApp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public class MessagesAppService
    {
        private readonly ChatDbContext chatDbContext;

        public MessagesAppService(ChatDbContext chatDbContext)
        {
            this.chatDbContext = chatDbContext;
        }

        public async Task<List<string>> GetRecievedMessages(int userId)
        {
            var messages = await chatDbContext.Messages
                .Where(message => message.RecipientUserId == userId)
                .ToListAsync();
            return messages
                .Select(message => message.Text)
                .ToList();
        }

        public async Task<int> CreateMessage(string message, int senderUserId, int recipientUserId)
        {
            var entity = new Message
            {
                Text = message,
                CreateDate = DateTime.Now,
                SenderUserId = senderUserId,
                RecipientUserId = recipientUserId
            };
            chatDbContext.Messages.Add(entity);
            await chatDbContext.SaveChangesAsync();
            return entity.Id;
        }
    }
}
