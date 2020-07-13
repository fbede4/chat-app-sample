using ChatApp.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Application.Interfaces
{
    public interface IUsersAppService
    {
        Task<UserDto> GetUser(int id);
        Task<int> CreateUser(string name);
        Task<List<UserDto>> GetUsers(string name);
        Task<UserDto> Login(string name);
    }
}
