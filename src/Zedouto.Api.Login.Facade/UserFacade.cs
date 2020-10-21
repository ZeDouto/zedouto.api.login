using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Zedouto.Api.Login.Facade.Interfaces;
using Zedouto.Api.Login.Model.Entities;
using Zedouto.Api.Login.Model.Models;
using Zedouto.Api.Login.Service.Interfaces;

namespace Zedouto.Api.Login.Facade
{
    public class UserFacade : IUserFacade
    {
        private readonly IUserService _userService;
        private readonly IJwtService<User> _jwtService;
        private readonly ICryptographyService _cryptographyService;
        
        public UserFacade(IUserService userService, IJwtService<User> jwtService, ICryptographyService cryptographyService)
        {
            _userService = userService;
            _jwtService = jwtService;
            _cryptographyService = cryptographyService;
        }

        public async Task AddUserAsync(User user)
        {
            var userInsert = new User
            {
                Login = user.Login,
                Password = await _cryptographyService.CryptographAsync(user.Password)
            };

            await _userService.AddUserAsync(userInsert);
        }

        public async Task<UserToken> LoginAsync(User user)
        {
            user.Password = await _cryptographyService.CryptographAsync(user.Password);
            
            var userLogged = await _userService.GetUserAsync(user);

            return userLogged is null
            ? null
            : await _jwtService.GetToken(user);
        }
    }
}
