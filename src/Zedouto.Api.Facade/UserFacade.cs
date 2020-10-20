using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Zedouto.Api.Facade.Interfaces;
using Zedouto.Api.Model.Entities;
using Zedouto.Api.Model.Models;
using Zedouto.Api.Service.Interfaces;

namespace Zedouto.Api.Facade
{
    public class UserFacade : IUserFacade
    {
        private readonly IUserService _userService;
        private readonly IJwtService<User> _jwtService;
        
        public UserFacade(IUserService userService, IJwtService<User> jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        public async Task AddUserAsync(User user)
        {
            await _userService.AddUserAsync(user);
        }

        public async Task<UserToken> LoginAsync(User user)
        {
            var userLogged = await _userService.GetUserAsync(user);

            if (userLogged is null)
            {
                return null;
            }

            return await _jwtService.GetToken(user);
        }
    }
}
