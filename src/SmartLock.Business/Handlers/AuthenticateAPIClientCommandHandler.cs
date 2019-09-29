using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using SmartLock.Business.Commands;
using SmartLock.Persistence.Contracts;

namespace SmartLock.Business.Handlers
{
    public class AuthenticateAPIClientCommandHandler : RequestHandler<AuthenticateAPIClientCommand, AuthenticateAPIClientResult>
    {
        private readonly IApiUserRepository apiUserRepository;

        public AuthenticateAPIClientCommandHandler(IApiUserRepository apiUserRepository)
        {
            this.apiUserRepository = apiUserRepository;
        }

        protected override AuthenticateAPIClientResult Handle(AuthenticateAPIClientCommand request)
        {
            var user = apiUserRepository.Get(request.Username, request.Password);

            if(user == null)
                return new AuthenticateAPIClientResult(false, null);

            // lets assume credentials are correct, so we generate a new jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(request.AppSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new []
                {
                    new Claim(ClaimTypes.Name, request.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(1),//lets make it easy to test
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticateAPIClientResult(true, tokenHandler.WriteToken(token));
        }
    }
}
