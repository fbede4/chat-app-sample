using ChatApp.Domain.Model;
using ChatApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
            return chatDbContext.Users
                .SingleAsync(u => u.Id == id);
        }

        public Task<User> GetUserAsync(string name)
        {
            return chatDbContext.Users
                .SingleOrDefaultAsync(u => u.Name == name);
        }

        public Task<List<User>> GetUsers(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return chatDbContext.Users
                    .ToListAsync();
            }
            return chatDbContext.Users
                .Where(u => u.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }
    }
}
