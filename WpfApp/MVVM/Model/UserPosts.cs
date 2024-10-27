using DAL.Concreate;
using DTO;
using MongoDB.Bson;

namespace WpfApp.MVVM.Model
{
    public class UserPosts
    {
        public PostDAL _postsDAL;
        public DBConnect dbConnect = new DBConnect();
        public UserPosts()
        {
            _postsDAL = new PostDAL(dbConnect.GetCollectionPosts());
        }
        public List<Post> GetPostsOfUser(ObjectId loggedUserID)
        {
            var userPosts = _postsDAL.GetAllByAuthorID(loggedUserID);
            return userPosts;
        }

    }
}
