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

UserDAL usersDAL = new UserDAL(users);
