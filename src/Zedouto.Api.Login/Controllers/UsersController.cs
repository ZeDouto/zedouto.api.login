using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zedouto.Api.Facade.Interfaces;
using Zedouto.Api.Login.Mapping;
using Zedouto.Api.Model.Entities;
using Zedouto.Api.Service.Interfaces;

namespace Zedouto.Api.Login.Controllers
{
    [ApiController]
    [Route(Routes.CONTROLLER_CONTEXT)]
    public class UsersController : ControllerBase
    {
        private readonly IUserFacade _userFacade;
        
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

            return Created($"{Routes.API_CONTEXT}/{nameof(UsersController).Replace("Controller", string.Empty)}/{Routes.LOGIN_CONTEXT}", user);
        }

        // <summary>
        // Return a User in database
        // </summary>
        [HttpPost(Routes.LOGIN_CONTEXT)]
        public async Task<IActionResult> LoginAsync([FromBody] User user)
        {
            var token = await _userFacade.LoginAsync(user);

            if (token is null)
            {
                return NoContent();
            }
            
            return Ok(token);
        }
    }
}