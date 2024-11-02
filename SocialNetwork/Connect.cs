//using DAL.Concreate;
//using DTO;
//using Microsoft.Extensions.Configuration;
//using MongoDB.Bson;
//using MongoDB.Driver;

//IConfiguration config = new ConfigurationBuilder()
//    .SetBasePath(Directory.GetCurrentDirectory())
//    .AddJsonFile("config.json")
//    .Build();

//string connectionString = config.GetConnectionString("DbConnection") ?? "";

//IMongoClient client = new MongoClient(connectionString);
//IMongoDatabase db = client.GetDatabase("SocialNetwork");

//var users = db.GetCollection<User>("users");
//var posts = db.GetCollection<Post>("posts");
//var comments = db.GetCollection<Comment>("comments");

//UserDAL usersDAL = new UserDAL(users);
//PostDAL postsDAL = new PostDAL(posts);
//CommentDAL commentsDAL = new CommentDAL(comments);

//postsDAL.Add(new Post
//{
//    ParentUserID = new ObjectId("670e88f54f32237ee312c59a"),
//    Title = "The Best Song",
//    Text = "Linkin Park - In The End",
//    Likes = 0,
//    Dislikes = 0,
//    Date = DateTime.Now
//});

//postsDAL.Add(new Post
//{
//    ParentUserID = new ObjectId("670e88f54f32237ee312c59a"),
//    Title = "The Second Best Song",
//    Text = "Linkin Park - Crawling",
//    Likes = 0,
//    Dislikes = 0,
//    Date = DateTime.Now
//});