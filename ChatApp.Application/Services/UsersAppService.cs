using ChatApp.Application.Dtos;
using ChatApp.Application.Interfaces;
using ChatApp.Domain.Configuration;
using ChatApp.Domain.Model;
using ChatApp.Domain.Repositories;
using ChatApp.Domain.UoW;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Application.Services
{
    public class UsersAppService : IUsersAppService
    {
        private readonly UserHandlingConfiguration config;
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;

        public UsersAppService(
            IOptions<UserHandlingConfiguration> options,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            this.config = options.Value;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<UserDto> GetUser(int id)
        {
            var user = await userRepository.GetUserAsync(id);
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name
            };
        }

        public async Task<int> CreateUser(string name)
        {
            if (config.IsUserCreationEnabled)
            {
                var entity = new User
                {
                    Name = name
                };
                userRepository.Insert(entity);
                await unitOfWork.SaveChangesAsync();
                return entity.Id;
            }
            throw new ValidationException("User creation is disabled");
        }

        public async Task<List<UserDto>> GetUsers(string name)
        {
            var users = await userRepository.GetUsers(name);
            return users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name
                }).ToList();
        }

        public async Task<UserDto> Login(string name)
        {
            var user = await userRepository.GetUserAsync(name);
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name
            };
        }
    }
}
