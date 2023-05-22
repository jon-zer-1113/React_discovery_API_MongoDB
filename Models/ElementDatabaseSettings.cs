namespace ReactMongoDB_API_01.Models
{
    public class ElementDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ElementsCollectionName { get; set; } = null!;
    }
}

// Ce modèle représente les paramètres de connexion à la BDD.

// Les propriétés de cette classe :
// - comme leurs noms l'indiquent...

// Ces propriétés correspondent au paramètres de connexion définis dans "appsettings.json".
