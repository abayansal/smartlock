using System;

namespace SmartLock.Persistence.Contracts
{
    public interface IGrantedAccessHistoryRepository
    {
        void Save(string userId, string gateId, DateTime grantDate);
    }
}