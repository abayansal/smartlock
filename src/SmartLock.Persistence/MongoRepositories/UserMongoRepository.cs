using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SmartLock.Domain;
using SmartLock.Persistence.Contracts;
using SmartLock.Persistence.Entities;

namespace SmartLock.Persistence.MongoRepositories
{
    public class UserMongoRepository : MongoRepository, IUserRepository
    {
        public UserMongoRepository(MongoClient mongoClient) : base(mongoClient, "smartlock", "users")
        {
        }

        public void Save(User user)
        {
            var entity = UserEntity.Load(user);

            var collection = MongoClient.GetDatabase(Database).GetCollection<UserEntity>(Collection);

            var filter = Builders<UserEntity>.Filter.Eq(x => x.Identity, user.Identity);
            collection.ReplaceOne(filter, entity, new UpdateOptions { IsUpsert = true });
        }

        public User Get(string id)
        {
            var collection = MongoClient.GetDatabase(Database).GetCollection<UserEntity>(Collection);

            var userEntity = collection.AsQueryable().Where(u => u.Identity == id).FirstOrDefault();

            return userEntity?.AsDomainObject();
        }
    }
}