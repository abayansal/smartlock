namespace SmartLock.Domain
{
    public class ApiUser
    {
        public string Username { get; }
        public string Password { get; }

        public ApiUser(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
