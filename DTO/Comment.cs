using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Comment
    {
        [BsonId]
        [BsonElement("commentID"), BsonRepresentation(BsonType.ObjectId)]
        public int CommentID { get; set; }

        [BsonElement("userID")]
        public int UserID { get; set; }

        [BsonElement("text"), BsonRepresentation(BsonType.String)]
        public string? Text { get; set; }

        [BsonElement("likes"), BsonRepresentation(BsonType.Int32)]
        public BsonArray Likes { get; set; } = new BsonArray();

        [BsonElement("dislikes"), BsonRepresentation(BsonType.Int32)]
        public BsonArray Dislikes { get; set; } = new BsonArray();

        [BsonElement("date"), BsonRepresentation(BsonType.DateTime)]
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
