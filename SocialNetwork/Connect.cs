using DAL.Concreate;
using DAL_Neo4j.Concreate;
using DAL_Neo4j;
using DTO;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("config.json")
    .Build();

string connectionString = config.GetConnectionString("DbConnection") ?? "";

IMongoClient client = new MongoClient(connectionString);
IMongoDatabase db = client.GetDatabase("SocialNetwork");

var users = db.GetCollection<User>("users");
//var posts = db.GetCollection<Post>("posts");
//var comments = db.GetCollection<Comment>("comments");

UserDAL usersDAL = new UserDAL(users);
//PostDAL postsDAL = new PostDAL(posts);
//CommentDAL commentsDAL = new CommentDAL(comments);

//async static Task TransferUsersFromMongoToNeo4J(List<User> users)
//{
//    Neo4JCommands cmd = new Neo4JCommands("bolt://localhost:7687", "neo4j", "12345678");
//    UserDAL_Neo4j userDAL_Neo4J = new UserDAL_Neo4j(cmd);
//    //adding users
//    for (int i = 0; i < users.Count; i++)
//    {
//        User userToTransfer = users[i];
//        await userDAL_Neo4J.Add(userToTransfer);
//    }
//    //adding relations
//    for (int i = 0; i < users.Count; i++)
//    {
//        User userToAddRelationFrom = users[i];
//        List<string> followings = new List<string>();
        
        
//        string userID = userToAddRelationFrom.UserID.ToString();
//        for (int j = 0; j < userToAddRelationFrom.FollowingIDs.Count; j++)
//        {
//            string userIDToFollow = userToAddRelationFrom.FollowingIDs[j].AsString;
//            await cmd.CreateRelationship("User", "User", "id", "id", userID, userIDToFollow, "FOLLOWS");
//            Console.WriteLine($"Added ({userToAddRelationFrom.UserName})-[FOLLOWS]->({userIDToFollow})");
//        }
//    }
//}

//await TransferUsersFromMongoToNeo4J(usersDAL.GetAll());