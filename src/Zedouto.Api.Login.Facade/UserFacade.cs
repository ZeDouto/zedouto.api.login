using System.Threading.Tasks;
using Zedouto.Api.Login.Facade.Interfaces;
using Zedouto.Api.Login.Model;
using Zedouto.Api.Login.Service.Interfaces;

namespace Zedouto.Api.Login.Facade
{
    public class UserFacade : IUserFacade
    {
        private readonly IUserService _userService;
        private readonly IJwtService<User, UserToken> _jwtService;
        
        public UserFacade(IUserService userService, IJwtService<User, UserToken> jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        public async Task AddUserAsync(User user)
        {
            await _userService.AddUserAsync(user);
        }

        public async Task<UserToken> GetByCpfAsync(string cpf)
        {
            var user = await _userService.GetUserAsync(new User { Cpf = cpf });

            return _jwtService.GetToken(user);
        }

        public async Task<UserToken> LoginAsync(User user)
        {            
            var userLogged = await _userService.GetByLoginAndSenhaAsync(user);

            if(userLogged is null)
            {
                return default;
            }

            return _jwtService.GetToken(userLogged);
        }
    }
}
