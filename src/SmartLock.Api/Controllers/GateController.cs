using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartLock.Api.DTOs;
using SmartLock.Business.Commands;
using SmartLock.Business.Exceptions;

namespace SmartLock.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GateController : ControllerBase
    {
        private readonly IMediator mediator;

        public GateController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody]GateDTO gateDto)
        {
            var command = new CreateGateCommand(gateDto.Identity, gateDto.Description);
            await mediator.Send(command);

            return Ok(new { message = "Gate created" });
        }

        [HttpPost("grant-access")]
        public async Task<IActionResult> GrantAccess([FromBody]UserAccessDTO userAccessDto)
        {
            try
            {
                var command = new GrantAccessCommand(userAccessDto.UserId, userAccessDto.GateId);
                await mediator.Send(command);

                return Ok(new {message = "Access granted to user"});
            }
            catch (MissingInformationProvidedException)
            {
                return BadRequest(new {message = "Please provide user and gate information properly" });
            }
            catch (GateDoesNotExistException)
            {
                return NotFound(new { message = "no such gate" });
            }
            catch (UserDoesNotExistException)
            {
                return NotFound(new { message = "no such user" });
            }
        }

        [HttpPost("unlock")]
        public async Task<IActionResult> UnlockGate([FromBody]UserAccessDTO userAccessDto)
        {
            try
            {
                var command = new UnlockGateCommand(userAccessDto.UserId, userAccessDto.GateId);
                var result = await mediator.Send(command);

                if (result.IsSuccess)
                {
                    return Ok(new { message = "Access granted :)" });
                }
                //since our operation is still a success with a negative result we return a HTTP_200
                return Ok(new { message = "You do not have the privileges to unlock this gate :(" });
            }
            catch (MissingInformationProvidedException)
            {
                return BadRequest("Please provide user and gate information properly");
            }
            catch (UserDoesNotExistException)
            {
                return NotFound(new { message = "no such user" });
            }
        }
    }
}