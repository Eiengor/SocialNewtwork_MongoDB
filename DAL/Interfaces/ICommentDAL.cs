using DTO;
using MongoDB.Bson;

namespace DAL.Interfaces
{
    public interface ICommentDAL
    {
        Comment GetByID(ObjectId ID);
        List<Comment> GetAll();
        void Add(Comment comment);
        void DeleteByID(ObjectId ID);
        void UpdateByID(ObjectId ID, Comment updatedComment);
    }
}
