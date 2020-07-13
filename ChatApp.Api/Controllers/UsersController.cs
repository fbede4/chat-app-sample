using ChatApp.Application.Dtos;
using ChatApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersAppService usersAppService;

        public UsersController(IUsersAppService usersAppService)
        {
            this.usersAppService = usersAppService;
        }

        [HttpGet("{id}")]
        public Task<UserDto> GetUser(int id)
        {
            return usersAppService.GetUser(id);
        }

        [HttpPost]
        public Task<int> CreateUser(string name)
        {
            return usersAppService.CreateUser(name);
        }
    }
}
