using DTO;
using MongoDB.Bson;

namespace DAL_Neo4j.Abstract
{
    public interface IUserDAL_Neo4j
    {
        Task Add(User user);
        Task DeleteByID(ObjectId ID);
        Task AddRelation(User user, ObjectId userToFollowID);
        Task DeleteRelation(User user, ObjectId userToUnfollowID);
    }
}
