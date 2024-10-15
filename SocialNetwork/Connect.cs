using DAL.Concreate;
using DTO;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("config.json")
    .Build();

string connectionString = config.GetConnectionString("DbConnection") ?? "";

IMongoClient client = new MongoClient(connectionString);
IMongoDatabase db = client.GetDatabase("SocialNetwork");

var users = db.GetCollection<User>("users");
var posts = db.GetCollection<Post>("posts");
var comments = db.GetCollection<Comment>("comments");

UserDAL usersDAL = new UserDAL(users);
PostDAL postsDAL = new PostDAL(posts);
CommentDAL commentsDAL = new CommentDAL(comments);