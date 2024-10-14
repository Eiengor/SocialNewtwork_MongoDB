using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace DTO
{
    public class Post
    {
        [BsonId]
        [BsonElement("postID"), BsonRepresentation(BsonType.ObjectId)]
        public int PostID { get; set; }

        [BsonElement("userID")]
        public int UserID { get; set; }

        [BsonElement("title"), BsonRepresentation(BsonType.String)]
        public string? Title { get; set; }

        [BsonElement("text"), BsonRepresentation(BsonType.String)]
        public string? Text { get; set; }

        [BsonElement("likes"), BsonRepresentation(BsonType.Int32)]
        public BsonArray Likes { get; set; } = new BsonArray();
        
        [BsonElement("dislikes"), BsonRepresentation(BsonType.Int32)]
        public BsonArray Dislikes { get; set; } = new BsonArray();
        
        [BsonElement("date"), BsonRepresentation(BsonType.DateTime)]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public override string ToString()
        {
            return $"{Title}:\n\t{Text}\n\t{Date}";
        }
    }
}
