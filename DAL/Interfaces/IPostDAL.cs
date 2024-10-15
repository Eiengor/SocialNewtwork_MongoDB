using DTO;
using MongoDB.Bson;

namespace DAL.Interfaces
{
    public interface IPostDAL
    {
        Post GetByID(ObjectId ID);
        List<Post> GetAll();
        void Add(Post post);
        void DeleteByID(ObjectId ID);
        void UpdateByID(ObjectId ID, Post updatedPost);
    }
}
