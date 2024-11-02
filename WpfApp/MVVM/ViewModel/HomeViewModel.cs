using DAL.Concreate;
using DTO;
using MongoDB.Bson;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp.MVVM.Model;

namespace WpfApp.MVVM.ViewModel
{


    public class HomeViewModel : INotifyPropertyChanged
    {
        private DBConnect dbConnect = new DBConnect();
        private UserDAL _userDAL;
        private PostDAL _postDAL;

        private List<StreamPostModel> _posts;
        public List<StreamPostModel> Posts
        {
            get => _posts;
            set
            {
                _posts = value;
                OnPropertyChanged(nameof(Posts));
            }
        }

        public HomeViewModel()
        {
            _postDAL = new PostDAL(dbConnect.GetCollectionPosts());
            _userDAL = new UserDAL(dbConnect.GetCollectionUsers());
            LoadPosts();
        }

        private void LoadPosts()
        {
            var posts = _postDAL.GetAll();
            var postModels = new List<StreamPostModel>();

            foreach (var post in posts)
            {
                var author = _userDAL.GetByID(post.ParentUserID);
                postModels.Add(new StreamPostModel
                {
                    PostID = post.PostID,
                    Title = post.Title,
                    Text = post.Text,
                    Likes = post.Likes,
                    Dislikes = post.Dislikes,
                    Date = post.Date,
                    AuthorName = author?.UserName ?? "Unknown"
                });
            }

            Posts = postModels;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
