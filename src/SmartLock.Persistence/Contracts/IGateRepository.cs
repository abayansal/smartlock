using SmartLock.Domain;

namespace SmartLock.Persistence.Contracts
{
    public interface IGateRepository
    {
        void Save(Gate gate);

        Gate Get(string id);
    }
}