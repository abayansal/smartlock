using System;
using MongoDB.Driver;
using SmartLock.Persistence.Contracts;
using SmartLock.Persistence.Entities;

namespace SmartLock.Persistence.MongoRepositories
{
    public class UnlockGateAttemptHistoryMongoRepository : MongoRepository, IUnlockGateAttemptHistoryRepository
    {
        public UnlockGateAttemptHistoryMongoRepository(MongoClient mongoClient) : base(mongoClient, "smartlock", "unlock-attempt-history")
        {
        }

        public void Save(string userId, string gateId, DateTime attemptDate, bool wasSuccess)
        {
            var entity = new UnlockGateAttemptEntity()
            {
                GateId = gateId,
                UserId = userId,
                AttemptDate = attemptDate,
                WasSuccess = wasSuccess
            };

            var collection = MongoClient.GetDatabase(Database).GetCollection<UnlockGateAttemptEntity>(Collection);

            collection.InsertOne(entity);
        }
    }
}