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
        public ObjectId CommentID { get; set; }

        [BsonElement("parentUserID")]
        public ObjectId ParentUserID { get; set; }
        
        [BsonElement("parentPostID")]
        public ObjectId ParentPostID { get; set;}

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
