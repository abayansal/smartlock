using MediatR;

namespace SmartLock.Business.Commands
{
    public class AuthenticateAPIClientCommand : IRequest<AuthenticateAPIClientResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string AppSecret { get; set; }

        public AuthenticateAPIClientCommand(string username, string password, string appSecret)
        {
            Username = username;
            Password = password;
            AppSecret = appSecret;
        }
    }

    public class AuthenticateAPIClientResult: IRequest
    {
        public bool IsSuccessful { get; }

        public string IssuedToken { get; }

        public AuthenticateAPIClientResult(bool isSuccessful, string issuedToken)
        {
            IsSuccessful = isSuccessful;
            IssuedToken = issuedToken;
        }
    }
}
