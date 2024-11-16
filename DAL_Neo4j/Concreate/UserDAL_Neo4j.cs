using DAL_Neo4j.Abstract;
using DTO;
using MongoDB.Bson;
using Neo4jClient.Cypher;

namespace DAL_Neo4j.Concreate
{
    public class UserDAL_Neo4j : IUserDAL_Neo4j
    {
        Neo4JCommands _cmd;
        public UserDAL_Neo4j(Neo4JCommands cmd)
        {
            _cmd = cmd;
        }
        async public Task Add(User user)
        {
            List<string> InterestsList = new List<string>();
            for (int i = 0; i < user.Interests.Count; i++)
            {
                InterestsList.Add(user.Interests[i].ToString());
            }

            var dictionaryUser = new Dictionary<string, object>
            {
                { "id", user.UserID.ToString() },
                { "firstName", user.FirstName },
                { "lastName", user.LastName },
                { "userName", user.UserName },
                { "email", user.Email },
                { "interests", InterestsList }
            };
            await _cmd.CreateNode("User", dictionaryUser);
        }

        async public Task AddRelation(User user, ObjectId userToFollowID)
        {
            string userID = user.UserID.ToString();
            await _cmd.CreateRelationship("User", "User", "id", "id", userID, userToFollowID.ToString(), "FOLLOWS");
        }
        async public Task DeleteRelation(User user, ObjectId userToUnfollowID)
        {
            string userID = user.UserID.ToString();
            await _cmd.DeleteRelationship("User", "User", "id", "id", userID, userToUnfollowID.ToString(), "FOLLOWS");
        }

        async public Task DeleteByID(ObjectId ID)
        {
            await _cmd.DetachDelete("User", "id", ID.ToString());
        }
    }
}
