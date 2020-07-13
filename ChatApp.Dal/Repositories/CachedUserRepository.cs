using ChatApp.Domain.Model;
using ChatApp.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace ChatApp.Dal.Repositories
{
    public class CachedUserRepository //: IUserRepository
    {
        private readonly IMemoryCache memoryCache;
        private readonly UserRepository userRepository;

        public CachedUserRepository(
            IMemoryCache memoryCache,
            UserRepository userRepository)
        {
            this.memoryCache = memoryCache;
            this.userRepository = userRepository;
        }

        public async Task<User> GetUserAsync(int id)
        {
            if (memoryCache.TryGetValue($"{nameof(User)}.{id}", out User user))
            {
                return user;
            }
            user = await userRepository.GetUserAsync(id);
            memoryCache.Set($"{nameof(User)}.{id}", user);
            return user;
        }

        public User Insert(User entity)
        {
            return userRepository.Insert(entity);
        }
    }
}
