using SmartLock.Domain;

namespace SmartLock.Persistence.Contracts
{
    public interface IApiUserRepository
    {
        ApiUser Get(string username, string password);
    }
}
