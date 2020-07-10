using ChatApp.Dal;
using ChatApp.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly ChatDbContext chatDbContext;

        public UsersController(ChatDbContext chatDbContext)
        {
            this.chatDbContext = chatDbContext;
        }

        [HttpPost]
        public async Task<int> CreateUser(string name)
        {
            var entity = new User
            {
                Name = name
            };
            chatDbContext.Users.Add(entity);
            await chatDbContext.SaveChangesAsync();
            return entity.Id;
        }
    }
}
