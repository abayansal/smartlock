using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SmartLock.Domain;
using SmartLock.Persistence.Contracts;
using SmartLock.Persistence.Entities;

namespace SmartLock.Persistence.MongoRepositories
{
    public class GateMongoRepository : MongoRepository, IGateRepository
    {
        public GateMongoRepository(MongoClient mongoClient) : base(mongoClient, "smartlock", "gates")
        {
        }

        public void Save(Gate gate)
        {
            var entity = GateEntity.Load(gate);

            var collection = MongoClient.GetDatabase(Database).GetCollection<GateEntity>(Collection);

            var filter = Builders<GateEntity>.Filter.Eq(x => x.Identity, gate.Identity);
            collection.ReplaceOne(filter, entity, new UpdateOptions { IsUpsert = true });
        }

        public Gate Get(string id)
        {
            var collection = MongoClient.GetDatabase(Database).GetCollection<GateEntity>(Collection);

            var gateEntity = collection.AsQueryable().Where(u => u.Identity == id).FirstOrDefault();

            return gateEntity?.AsDomainObject();
        }
    }
}