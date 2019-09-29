using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartLock.Api.DTOs;
using SmartLock.Business.Commands;

namespace SmartLock.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly AppSettings appSettings;

        public TokenController(IMediator mediator, AppSettings appSettings)
        {
            this.mediator = mediator;
            this.appSettings = appSettings;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]ApiUserDTO apiUserDto)
        {
            var command = new AuthenticateAPIClientCommand(apiUserDto.Username, apiUserDto.Password, appSettings.Secret);
            var result = await mediator.Send(command);

            if (result.IsSuccessful)
            {
                apiUserDto.Token = result.IssuedToken;
                return Ok(apiUserDto);
            }

            return Unauthorized(new { message = "Username or password is incorrect" });
        }
    }
}