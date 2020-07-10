using ChatApp.Dal;
using ChatApp.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public class MessagesAppService
    {
        private readonly ILogger<MessagesAppService> logger;
        private readonly ChatDbContext chatDbContext;

        public MessagesAppService(
            ILogger<MessagesAppService> logger,
            ChatDbContext chatDbContext)
        {
            this.logger = logger;
            this.chatDbContext = chatDbContext;
        }

        public async Task<List<string>> GetRecievedMessages(int userId)
        {
            logger.LogInformation($"Gathering messages for user {userId}");
            var messages = await chatDbContext.Messages
                .Where(message => message.RecipientUserId == userId)
                .ToListAsync();
            logger.LogInformation($"Gathered messages for user {userId}");
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
