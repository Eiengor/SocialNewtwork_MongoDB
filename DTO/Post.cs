using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace DTO
{
    public class Post
    {
        [BsonId]
        [BsonElement("postID"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId PostID { get; set; }

        [BsonElement("parentUserID")]
        public ObjectId ParentUserID { get; set; }

        [BsonElement("title"), BsonRepresentation(BsonType.String)]
        public string? Title { get; set; }

        [BsonElement("text"), BsonRepresentation(BsonType.String)]
        public string? Text { get; set; }

        [BsonElement("likes"), BsonRepresentation(BsonType.Int32)]
        public int Likes { get; set; }
        
        [BsonElement("dislikes"), BsonRepresentation(BsonType.Int32)]
        public int Dislikes { get; set; }
        
        [BsonElement("date"), BsonRepresentation(BsonType.DateTime)]
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
