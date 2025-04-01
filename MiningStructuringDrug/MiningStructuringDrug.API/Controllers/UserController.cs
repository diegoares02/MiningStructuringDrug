using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiningStructuringDrug.Core.Application.Common;
using MiningStructuringDrug.Core.Application.Users.Commands;
using MiningStructuringDrug.Core.Application.Users.Dtos;
using MiningStructuringDrug.Core.Application.Users.Queries;

namespace MiningStructuringDrug.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IMessageDispatcher _dispatcher;

        public UserController(IMessageDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
        {
            var result = await _dispatcher.DispatchCommand<CreateUserCommand, UserDto>(command);
            return CreatedAtAction(nameof(GetUser), new { id = result.Id }, result);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserCommand command)
        {
            var result = await _dispatcher.DispatchCommand<AuthenticateUserCommand, AuthResponseDto?>(command);

            if (result == null)
            {
                return Unauthorized();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUser(int id)
        {
            var query = new GetUserQuery { Id = id };
            var result = await _dispatcher.DispatchQuery<GetUserQuery, UserDto?>(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var command = new DeleteUserCommand { Id = id };
            await _dispatcher.Dispatch<DeleteUserCommand>(command);
            return NoContent();
        }
        
        [HttpPut("{id}/role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserRole(int id, [FromBody] UpdateUserRoleCommand command)
        {
            command.Id = id;
            await _dispatcher.Dispatch<UpdateUserRoleCommand>(command);
            return NoContent();
        }
    }
}
