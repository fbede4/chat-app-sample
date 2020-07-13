using ChatApp.Domain.Model;
using ChatApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ChatApp.Dal.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ChatDbContext chatDbContext) : base(chatDbContext)
        {
        }

        public Task<User> GetUserAsync(int id)
        {
            return chatDbContext.Users.SingleAsync(u => u.Id == id);
        }
    }
}
