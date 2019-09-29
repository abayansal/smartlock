using MongoDB.Driver;

namespace SmartLock.Persistence.MongoRepositories
{
    public abstract class MongoRepository
    {
        protected readonly MongoClient MongoClient;
        protected readonly string Database;
        protected readonly string Collection;

        protected MongoRepository(MongoClient mongoClient, string database, string collection)
        {
            MongoClient = mongoClient;
            Database = database;
            Collection = collection;
        }
    }
}