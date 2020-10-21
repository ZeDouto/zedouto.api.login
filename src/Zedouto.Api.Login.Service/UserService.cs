using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zedouto.Api.Login.Model.Entities;
using Zedouto.Api.Login.Repository.Interfaces;
using Zedouto.Api.Login.Service.Interfaces;

namespace Zedouto.Api.Login.Service
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

        public async Task<User> GetUserAsync(User user)
        {
            return await _repository.GetAsync(new Dictionary<string, object> {
                { nameof(user.Login), user.Login },
                { nameof(user.Password), user.Password }
            });
        }
    }
}
