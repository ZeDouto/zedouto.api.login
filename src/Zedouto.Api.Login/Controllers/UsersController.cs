using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zedouto.Api.Login.Facade.Interfaces;
using Zedouto.Api.Login.Mapping;
using Zedouto.Api.Login.Model;

namespace Zedouto.Api.Login.Controllers
{
    [ApiController]
    [Route(Routes.CONTROLLER_CONTEXT)]
    public class UsersController : ControllerBase
    {
        private readonly IUserFacade _userFacade;
        private const string INVALID_PARAMETER_TEXT = "Parâmetros inválidos, favor validar o corpo da requisição";
        
        public UsersController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        // <summary>
        // Create a user in database
        // </summary>
        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody] User user)
        {            
            await _userFacade.AddUserAsync(user);

            return Created(string.Empty, user);
        }

        // <summary>
        // Return a User
        // </summary>
        [HttpPost(Routes.LOGIN_CONTEXT)]
        public async Task<IActionResult> LoginAsync([FromBody] User user)
        {
            var token = await _userFacade.LoginAsync(user);

            if(IsValidUserToken(token))
            {
                return Ok(token);
            }

            return NoContent();
        }

        // <summary>
        // Return a User by login
        // </summary>
        [HttpGet("{cpf}")]
        public async Task<IActionResult> GetAsync([FromRoute] string cpf)
        {
            var token = await _userFacade.GetByCpfAsync(cpf);

            if(IsValidUserToken(token))
            {
                return Ok(token);
            }

            return NoContent();
        }

        [HttpGet("token")]
        public async Task<IActionResult> GetTokenVaueAsync([FromHeader(Name = "token")] string token)
        {
            var user = await _userFacade.DeserializeTokenAsync(token);

            if(IsValidUserToken(user))
            {
                return Ok(user);
            }

            return NoContent();
        }

        [HttpGet("doctors")]
        public async Task<IActionResult> GetDoctorsAsync([FromQuery(Name = "crms")] string crms)
        {
            var users = await _userFacade.GetAllByCrmsAsync(crms.Split(','));

            if(users.Any() && users.All(user => IsValidUserToken(user)))
            {
                return Ok(users);
            }

            return NoContent();
        }

        private bool IsValidUserToken(UserToken user)
        {
            return user != default;
        }

        private bool IsValidUserToken(User user)
        {
            return user != default;
        }
    }
}