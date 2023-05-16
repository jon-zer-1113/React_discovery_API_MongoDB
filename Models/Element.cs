using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReactMongoDB_API_01.Models
{
    public class Element
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("txt")]
        public string Txt { get; set; } = null!;
    }
}

// L'attribut BsonId indique que cette propriété est l'indentifiant unique de l'élément.
// BsonRepresentation : spécifie que l'identifiant est représenté comme un ObjectId dans la base de données
// BsonElement spécifie que cette propriété correspond au champ "txt" dans le document MongoDB (conversion de Txt en txt)