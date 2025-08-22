using Microservice.basvuru.domain.Entity;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace Microservice.basvuru.api
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration["MongoSettings:ConnectionString"];
            var databaseName = configuration["MongoSettings:DatabaseName"];
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<MusteriBasvuru> Basvurular => _database.GetCollection<MusteriBasvuru>("Basvurular");
    }
}
