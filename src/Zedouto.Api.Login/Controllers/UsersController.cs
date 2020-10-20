using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zedouto.Api.Model.Entities;
using Zedouto.Api.Service.Interfaces;

namespace Zedouto.Api.Login.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

            return CreatedAtAction(nameof(LoginAsync), user);
        }

        // <summary>
        // Return a User in database
        // </summary>
        [HttpGet]
        public async Task<IActionResult> LoginAsync([FromQuery] User user)
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