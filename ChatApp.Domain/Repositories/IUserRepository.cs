using ChatApp.Domain.Model;
using System.Threading.Tasks;

namespace ChatApp.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserAsync(int id);
    }
}
