using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DTO
{
    public class User
    {
        [BsonId]
        [BsonElement("userID"), BsonRepresentation(BsonType.ObjectId)]
        public int UserID { get; set; }
        
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

        [BsonElement("interests"), BsonRepresentation(BsonType.String)]
        public BsonArray Interests { get; set; } = new BsonArray();

        public override string ToString()
        {
            return $"{UserName}:\n\t{FirstName} {LastName}|  {Email}\n\tInterests: {Interests}";
        }
    }
}
