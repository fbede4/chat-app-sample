using ChatApp.Application.Dtos;
using System.Threading.Tasks;

namespace ChatApp.Application.Interfaces
{
    public interface IUsersAppService
    {
        Task<UserDto> GetUser(int id);
        Task<int> CreateUser(string name);
    }
}
