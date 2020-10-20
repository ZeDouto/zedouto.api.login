using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zedouto.Api.Login.Mapping;
using Zedouto.Api.Model.Entities;
using Zedouto.Api.Service.Interfaces;

namespace Zedouto.Api.Login.Controllers
{
    [ApiController]
    [Route(Routes.CONTROLLER_CONTEXT)]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // <summary>
        // Create a user in database
        // </summary>
        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody] User user)
        {
            await _userService.AddUserAsync(user);

            return Created($"{Routes.API_CONTEXT}/{nameof(UsersController).Replace("Controller", string.Empty)}/{Routes.LOGIN_CONTEXT}", user);
        }

        // <summary>
        // Return a User in database
        // </summary>
        [HttpPost(Routes.LOGIN_CONTEXT)]
        public async Task<IActionResult> LoginAsync([FromBody] User user)
        {
            var userLogged = await _userService.GetUserAsync(user);

            if (userLogged is null)
            {
                return NoContent();
            }
            
            return Ok(userLogged);
        }
    }
}