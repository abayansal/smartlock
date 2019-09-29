using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartLock.Api.DTOs;
using SmartLock.Business.Commands;

namespace SmartLock.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody]UserDTO userDto)
        {
            var command = new CreateUserCommand(userDto.FirstName, userDto.LastName, userDto.Identity);
            await mediator.Send(command);

            return Ok(new { message = "User created" });
        }
    }
}