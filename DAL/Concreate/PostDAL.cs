using DAL.Interfaces;
using DTO;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DAL.Concreate
{
    public class PostDAL : IPostDAL
    {
        private readonly IMongoCollection<Post> _posts;
        public PostDAL(IMongoCollection<Post> posts)
        {
            _posts = posts;
        }
        public void Add(Post post)
        {
            if(string.IsNullOrEmpty(post.Text))
            {
                throw new Exception("Post is empty");
            }
            if(string.IsNullOrEmpty(post.Title))
            {
                throw new Exception("Title is empty");
            }
            _posts.InsertOne(post);
        }

        public void DeleteByID(ObjectId ID)
        {
            var postToDelete = Builders<Post>.Filter.Eq(p => p.PostID, ID);
            _posts.DeleteOneAsync(postToDelete);
        }

        public List<Post> GetAll()
        {
            return _posts.Find(Builders<Post>.Filter.Empty).ToList();
        }
        public List<Post> GetAllByAuthorID(ObjectId authorID)
        {
            return _posts.Find(post => post.ParentUserID == authorID).ToList();
        }

        public Post GetByID(ObjectId ID)
        {
            return _posts.Find<Post>(p => p.PostID == ID).FirstOrDefault();
        }

        public void UpdateByID(ObjectId ID, Post updatedPost)
        {
            var filter = Builders<Post>.Filter.Eq(p => p.PostID, ID);
            var update = Builders<Post>.Update.
                Set(p => p.PostID, updatedPost.PostID).
                Set(p => p.ParentUserID, updatedPost.ParentUserID).
                Set(p => p.Title, updatedPost.Title).
                Set(p => p.Text, updatedPost.Text).
                Set(p => p.Likes, updatedPost.Likes).
                Set(p => p.Dislikes, updatedPost.Dislikes);
            _posts.UpdateOne(filter, update);
        }
    }
}
