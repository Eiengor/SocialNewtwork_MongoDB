using DAL_Neo4j;
using DAL_Neo4j.Concreate;
using DTO;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.IO;

namespace WpfApp.MVVM.Model
{
    public class DBConnect
    {
        public IMongoDatabase db;
        public Neo4JCommands cmd;
        public DBConnect()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                .Build();

            string connectionString = config.GetConnectionString("DbConnection") ?? "";

            IMongoClient client = new MongoClient(connectionString);
            db = client.GetDatabase("SocialNetwork");
            cmd = new Neo4JCommands("bolt://localhost:7687", "neo4j", "12345678");

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
        public UserDAL_Neo4j GetNeo4JCollectionUsers()
        {
            var users = new UserDAL_Neo4j(cmd);
            return users;
        }
    }
}
