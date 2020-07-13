using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Application.Interfaces
{
    public interface IMessagesAppService
    {
        Task<List<string>> GetRecievedMessages(int userId);
        Task<int> CreateMessage(string message, int senderUserId, int recipientUserId);
    }
}
