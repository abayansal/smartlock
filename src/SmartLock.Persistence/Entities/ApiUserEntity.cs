using SmartLock.Domain;

namespace SmartLock.Persistence.Entities
{
    public class ApiUserEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public ApiUser AsDomainObject()
        {
            return new ApiUser(Username, Password);
        }
    }
}
