using System;
using MongoDB.Driver;
using SmartLock.Persistence.Contracts;
using SmartLock.Persistence.Entities;

namespace SmartLock.Persistence.MongoRepositories
{
    public class GrantedAccessHistoryMongoRepository : MongoRepository, IGrantedAccessHistoryRepository
    {
        public GrantedAccessHistoryMongoRepository(MongoClient mongoClient) : base(mongoClient, "smartlock", "granted-access-history")
        {
        }

        public void Save(string userId, string gateId, DateTime grantDate)
        {
            var entity = new GrantedAccessHistoryEntity()
            {
                GateId = gateId,
                UserId = userId,
                GrantDate = grantDate
            };

            var collection = MongoClient.GetDatabase(Database).GetCollection<GrantedAccessHistoryEntity>(Collection);

            collection.InsertOne(entity);
        }
    }
}