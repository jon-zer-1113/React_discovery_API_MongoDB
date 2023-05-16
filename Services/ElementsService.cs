using ReactMongoDB_API_01.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ReactMongoDB_API_01.Services
{
    public class ElementsService
    {
        private readonly IMongoCollection<Element> _elementsCollection;

        public ElementsService(
            IOptions<ElementDatabaseSettings> elementDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                elementDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                elementDatabaseSettings.Value.DatabaseName);

            _elementsCollection = mongoDatabase.GetCollection<Element>(
                elementDatabaseSettings.Value.ElementsCollectionName);
        }

        public async Task<List<Element>> GetAsync() =>
            await _elementsCollection.Find(_ => true).ToListAsync();

        public async Task<Element?> GetAsync(string id) =>
            await _elementsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Element newElement) =>
            await _elementsCollection.InsertOneAsync(newElement);

        public async Task UpdateAsync(string id, Element updatedElement) =>
            await _elementsCollection.ReplaceOneAsync(x => x.Id == id, updatedElement);

        public async Task RemoveAsync(string id) =>
            await _elementsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
