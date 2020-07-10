using ChatApp.Configuration;
using ChatApp.Dal;
using ChatApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly UserHandlingConfiguration config;
        private readonly ChatDbContext chatDbContext;

        public UsersController(
            IOptions<UserHandlingConfiguration> options,
            ChatDbContext chatDbContext)
        {
            this.config = options.Value;
            this.chatDbContext = chatDbContext;
        }

        [HttpPost]
        public async Task<int> CreateUser(string name)
        {
            if (config.IsUserCreationEnabled)
            {
                var entity = new User
                {
                    Name = name
                };
                chatDbContext.Users.Add(entity);
                await chatDbContext.SaveChangesAsync();
                return entity.Id;
            }
            throw new ValidationException("User creation is disabled");
        }
    }
}
