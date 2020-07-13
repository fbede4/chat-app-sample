using ChatApp.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserAsync(int id);
        Task<User> GetUserAsync(string name);
        Task<List<User>> GetUsers(string name);
    }
}
