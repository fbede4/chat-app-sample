using System.Threading.Tasks;

namespace ChatApp.Application.Interfaces
{
    public interface IUsersAppService
    {
        Task<int> CreateUser(string name);
    }
}
