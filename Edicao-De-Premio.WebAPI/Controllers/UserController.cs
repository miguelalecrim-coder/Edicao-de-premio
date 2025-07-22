using Application.DTO;
using Application.Interfaces;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        
        [HttpPost]
        public async Task<ActionResult<IUser>> Create([FromBody] Guid userId )
        {
            var user = await _userService.AddUserReferenceAsync(userId);

            if (user == null)
                return BadRequest("Não foi possível criar o utilizador.");

            return Ok(user);
        }
    }
}
