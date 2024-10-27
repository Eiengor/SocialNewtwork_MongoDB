using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DTO
{
    public class User
    {
        [BsonId]
        [BsonElement("userID"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId UserID { get; set; }
        
        [BsonElement("firstName"), BsonRepresentation(BsonType.String)]
        public string? FirstName { get; set; }
        
        [BsonElement("lastName"), BsonRepresentation(BsonType.String)]
        public string? LastName { get; set;}

        [BsonElement("userName"), BsonRepresentation(BsonType.String)]
        public string? UserName { get; set; }

        [BsonElement("email"), BsonRepresentation(BsonType.String)]
        public string? Email { get; set; } = null;

        [BsonElement("password"), BsonRepresentation(BsonType.String)]
        public string? Password { get; set; }

        [BsonElement("interests")]
        public BsonArray Interests { get; set; } = new BsonArray();
    }
}
