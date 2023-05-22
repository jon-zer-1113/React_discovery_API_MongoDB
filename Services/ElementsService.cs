using ReactMongoDB_API_01.Models;
using Microsoft.Extensions.Options; // Permet de récupérer les options de config du modele "ElementDatabaseSettings".
using MongoDB.Driver; // Pour interagir avec MongoDB.

namespace ReactMongoDB_API_01.Services
{
    public class ElementsService
    {
        private readonly IMongoCollection<Element> _elementsCollection;
        // Variable privée + en lecture seule, + de type "IMongoCollection<Element>",
        // donc _elementsCollection est la variable que j'utilise pour interagir avec ma collection MongoDB.

        public ElementsService(
            IOptions<ElementDatabaseSettings> elementDatabaseSettings)
            // Constructeur de la classe de ce fichier. (en paramètre: une instance pour accéder aux options de config).
        {
            var mongoClient = new MongoClient(
                elementDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                elementDatabaseSettings.Value.DatabaseName);

            _elementsCollection = mongoDatabase.GetCollection<Element>(
                elementDatabaseSettings.Value.ElementsCollectionName);
            // Dans le constructeur on a :
            // récupéré la chaîne de connexion, la base de données et la collection à partir de elementDatabaseSettings pour établir la connexion à MongoDB,
            // et obtenu une référence à la collection "Element".
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
/*
Pour finir, toutes les méthodes asynchrone sont définis comme suit (avec des différences pour certaines):
1: Le contenu sur lequel la Task agit (List<Element>, Elements?, CreateAsync, etc...).
2: Le nom de la méthode (GetAsync, CreateAsync, etc...) avec ses paramètres ou non (string id...).
3: Ensuite "await" suspend l'exécution de la méthode (ex: GetAsync) jusqu'à ce que le reste (ex: ToListAsync) soit terminée.
4: Ce "reste" c'est ce que l'on veut exécuter avec la méthode en question (GetAsync).

exemple: " await _elementsCollection.Find(_ => true).ToListAsync() "
    - " Find " est utilisée pour spécifier la condition de recherche.
    - " _ " est une variable anonyme qui représente chaque document de la collection. 
    - " => true " est une expression lambda qui retourne toujours true, ce qui signifie que tous les documents de la collection seront sélectionnés.
    - " ToListAsync() " méthode est utilisée pour exécuter la requête de recherche de manière asynchrone et récupérer les résultats sous forme de liste d'éléments (List<Element>).
Une fois que la tâche ToListAsync() est terminée, le contrôle est retourné à la méthode GetAsync() et la liste d'éléments récupérée à partir de la requête de recherche MongoDB est renvoyée comme résultat de la tâche.

En gros, toutes ces méthodes agissent sur le CRUD (GetAll, Get, Create, Put, Delete).
*/