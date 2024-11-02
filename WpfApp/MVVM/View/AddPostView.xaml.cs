using DAL.Concreate;
using DTO;
using MongoDB.Bson;
using System.Windows;
using System.Windows.Controls;
using WpfApp.MVVM.Model;

namespace WpfApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for AddPostView.xaml
    /// </summary>
    public partial class AddPostView : UserControl
    {
        //private readonly PostDAL _postDAL;
        private DBConnect dbConnect = new DBConnect();
        public AddPostView()
        {
            InitializeComponent();
        }
        private void AddPostButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(postTitle.Text)) // check if the title is empty
            {
                MessageBox.Show("Please enter a post title!", "Wrong Title!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var _postDAL = new PostDAL(dbConnect.GetCollectionPosts());
                var newPost = new Post
                {
                    ParentUserID = UserSession.LoggedInUserId,
                    Title = postTitle.Text,
                    Text = postText.Text,
                    Likes = 0,
                    Dislikes = 0,
                    Date = DateTime.Now,
                };
                _postDAL.Add(newPost);
                postTitle.Text = "";
                postText.Text = "";
            }
        }
    }
}
