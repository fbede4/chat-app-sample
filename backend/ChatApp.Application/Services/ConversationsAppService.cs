using ChatApp.Application.Dtos;
using ChatApp.Application.Interfaces;
using ChatApp.Domain.Model;
using ChatApp.Domain.Repositories;
using ChatApp.Domain.UoW;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Application.Services
{
    public class ConversationsAppService : IConversationsAppService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConversationRepository conversationRepository;

        public ConversationsAppService(
            IUnitOfWork unitOfWork,
            IConversationRepository conversationRepository)
        {
            this.unitOfWork = unitOfWork;
            this.conversationRepository = conversationRepository;
        }

        public async Task<List<ConversationListDto>> GetConversations(int userId)
        {
            var conversations = await conversationRepository.GetConversations(userId);
            return conversations
                .Select(c => new ConversationListDto
                {
                    Id = c.Id,
                    PartnerUserName = c.FirstParticipantUserId == userId
                        ? c.SecondParticipantUser.Name
                        : c.FirstParticipantUser.Name,
                    LastMessage = c.Messages
                        .OrderByDescending(m => m.CreateDate)
                        .FirstOrDefault()?.Text ?? ""
                }).ToList();
        }

        public async Task<ConversationDto> GetConversation(int conversationId, int userId)
        {
            var conversation = await conversationRepository.GetConversation(conversationId);
            return new ConversationDto
            {
                Id = conversation.Id,
                PartnerUserName = conversation.FirstParticipantUserId == userId
                        ? conversation.SecondParticipantUser.Name
                        : conversation.FirstParticipantUser.Name,
                Messages = conversation.Messages
                    .Select(m => new MessageDto
                    {
                        IsSent = m.SentByUserId == userId,
                        Message = m.Text,
                        TimeStamp = m.CreateDate
                    }).ToList()
            };
        }

        public async Task<int> CreateConversation(ConversationCreateDto dto)
        {
            if (await conversationRepository.GetIfExists(dto.FirstUserId, dto.SecondUserId))
            {
                return 0;
            }
            var conversation = new Conversation
            {
                FirstParticipantUserId = dto.FirstUserId,
                SecondParticipantUserId = dto.SecondUserId
            };
            conversationRepository.Insert(conversation);
            await unitOfWork.SaveChangesAsync();
            return conversation.Id;
        }
    }
}
