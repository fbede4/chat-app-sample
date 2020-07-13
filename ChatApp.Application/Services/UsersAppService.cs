using ChatApp.Application.Interfaces;
using ChatApp.Domain.Configuration;
using ChatApp.Domain.Model;
using ChatApp.Domain.Repositories;
using ChatApp.Domain.UoW;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
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
    }
}
