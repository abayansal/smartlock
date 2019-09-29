using SmartLock.Domain;

namespace SmartLock.Persistence.Contracts
{
    public interface IUserRepository
    {
        void Save(User user);

        User Get(string id);
    }
}