using ChatApp.Application.Interfaces;
using ChatApp.Domain.Model;
using ChatApp.Domain.Repositories;
using ChatApp.Domain.UoW;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Application.Services
{
    public class MessagesAppService : IMessagesAppService
    {
        private readonly ILogger<MessagesAppService> logger;
        private readonly IMessageRepository messageRepository;
        private readonly IUnitOfWork unitOfWork;

        public MessagesAppService(
            ILogger<MessagesAppService> logger,
            IMessageRepository messageRepository,
            IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.messageRepository = messageRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<string>> GetRecievedMessages(int userId)
        {
            logger.LogInformation($"Gathering messages for user {userId}");
            var messages = await messageRepository.GetMessages(message => message.RecipientUserId == userId);
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
            messageRepository.Insert(entity);
            await unitOfWork.SaveChangesAsync();
            return entity.Id;
        }
    }
}
