using System;
using System.Threading.Tasks;
using Zedouto.Api.Model.Entities;
using Zedouto.Api.Repository.Interfaces;
using Zedouto.Api.Service.Interfaces;

namespace Zedouto.Api.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        
        public UserService(IRepository<User> repository)
        {
            _repository = repository;   
        }
        
        public async Task AddUserAsync(User user)
        {
            await _repository.AddAsync(user);
        }
    }
}
