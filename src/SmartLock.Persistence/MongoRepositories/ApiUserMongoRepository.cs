using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SmartLock.Domain;
using SmartLock.Persistence.Contracts;
using SmartLock.Persistence.Entities;

namespace SmartLock.Persistence.MongoRepositories
{
    public class ApiUserMongoRepository : MongoRepository, IApiUserRepository
    {
        public ApiUserMongoRepository(MongoClient mongoClient) : base(mongoClient, "smartlock", "api-users")
        {
        }

        public ApiUser Get(string username, string password)
        {
            var collection = MongoClient.GetDatabase(Database).GetCollection<ApiUserEntity>(Collection);

            var apiUserEntity = collection.AsQueryable().Where(u => u.Username == username && u.Password == password).FirstOrDefault();

            if (apiUserEntity != null)
            {
                return apiUserEntity.AsDomainObject();
            }

            return null;
        }
    }
}
