using System.Threading.Tasks;

namespace ChatApp.Application.Interfaces
{
    public interface IMessagesAppService
    {
        Task SendMessage(string message, int sentByUserId, int conversationId);
    }
}
