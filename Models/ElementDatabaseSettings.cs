namespace ReactMongoDB_API_01.Models
{
    public class ElementDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ElementsCollectionName { get; set; } = null!;
    }
}
