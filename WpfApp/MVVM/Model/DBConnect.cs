using DTO;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.IO;

namespace WpfApp.MVVM.Model
{
    public class DBConnect
    {
        public IMongoDatabase db;
        public DBConnect()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                .Build();

            string connectionString = config.GetConnectionString("DbConnection") ?? "";

            IMongoClient client = new MongoClient(connectionString);
            db = client.GetDatabase("SocialNetwork");
        }

        public IMongoCollection<User> GetCollectionUsers()
        {
            var users = db.GetCollection<User>("users");
            return users;
        }
        public IMongoCollection<Post> GetCollectionPosts()
        {
            var posts = db.GetCollection<Post>("posts");
            return posts;
        }
        public IMongoCollection<Comment> GetCollectionComments()
        {
            var comments = db.GetCollection<Comment>("comments");
            return comments;
        }
    }
}
