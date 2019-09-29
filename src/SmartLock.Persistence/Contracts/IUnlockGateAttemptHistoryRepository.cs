using System;

namespace SmartLock.Persistence.Contracts
{
    public interface IUnlockGateAttemptHistoryRepository
    {
        void Save(string userId, string gateId, DateTime attemptDate, bool wasSuccess);
    }
}