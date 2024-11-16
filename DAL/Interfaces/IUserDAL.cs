using DTO;
using MongoDB.Bson;

namespace DAL.Abstract
{
    public interface IUserDAL
    {
        User GetByID(ObjectId ID);
        User GetByUserName(string userName);
        List<User> GetAll();
        void Add(User user);
        void DeleteByID(ObjectId ID);
        void Update(ObjectId ID, User updatedUser);
        bool UserExists(string username);
        void AddFollower(ObjectId userId, ObjectId followerID);
        void AddFollowing(ObjectId userId, ObjectId followingID);
        void DeleteFollower(ObjectId userId, ObjectId followerID);
        void DeleteFollowing(ObjectId userId, ObjectId followingID);
    }
}
