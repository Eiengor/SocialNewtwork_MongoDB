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
    }
}
