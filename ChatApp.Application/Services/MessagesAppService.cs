using ChatApp.Application.Interfaces;
using ChatApp.Domain.Model;
using ChatApp.Domain.Repositories;
using ChatApp.Domain.UoW;
using Microsoft.Extensions.Logging;
using System;
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

        public async Task SendMessage(string message, int sentByUserId, int conversationId)
        {
            var entity = new Message
            {
                Text = message,
                CreateDate = DateTime.Now,
                SentByUserId = sentByUserId,
                ConversationId = conversationId
            };
            messageRepository.Insert(entity);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
