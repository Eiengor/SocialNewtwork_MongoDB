using DAL.Interfaces;
using DTO;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DAL.Concreate
{
    public class CommentDAL : ICommentDAL
    {
        private readonly IMongoCollection<Comment> _comments;
        public CommentDAL(IMongoCollection<Comment> comments)
        {
            _comments = comments;
        }
        public void Add(Comment comment)
        {
            if(string.IsNullOrEmpty(comment.Text))
            {
                throw new Exception("Comment is empty");
            }
            _comments.InsertOne(comment);
        }

        public void DeleteByID(ObjectId ID)
        {
            var commentToDelete = Builders<Comment>.Filter.Eq(c => c.CommentID, ID);
            _comments.DeleteOneAsync(commentToDelete);
        }

        public List<Comment> GetAll()
        {
            return _comments.Find(Builders<Comment>.Filter.Empty).ToList();
        }

        public Comment GetByID(ObjectId ID)
        {
            return _comments.Find<Comment>(c => c.CommentID == ID).FirstOrDefault();
        }

        public void UpdateByID(ObjectId ID, Comment updatedComment)
        {
            var filter = Builders<Comment>.Filter.Eq(c => c.CommentID, ID);
            var update = Builders<Comment>.Update.
                Set(c => c.CommentID, updatedComment.CommentID).
                Set(c => c.ParentUserID, updatedComment.ParentUserID).
                Set(c => c.ParentPostID, updatedComment.ParentPostID).
                Set(c => c.Text, updatedComment.Text).
                Set(c => c.Likes, updatedComment.Likes).
                Set(c => c.Dislikes, updatedComment.Dislikes);
            _comments.UpdateOne(filter, update);
        }
    }
}
