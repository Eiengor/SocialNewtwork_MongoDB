using DAL.Concreate;
using DTO;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using WpfApp.MVVM.Model;

namespace WpfApp.MVVM.ViewModel
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        public UserDAL _userDAL;
        public ObjectId _loggedInUserId;
        public User _user;
        public List<Post> _userPosts;
        public DBConnect dbConnect = new DBConnect();
        public ProfileViewModel()
        {           
            _userDAL = new UserDAL(dbConnect.GetCollectionUsers());
            _loggedInUserId = new ObjectId("670e88f54f32237ee312c59a");
            LoadUserData(_loggedInUserId);
            _userPosts = new UserPosts().GetPostsOfUser(_loggedInUserId);
        }

        public void LoadUserData(ObjectId userId)
        {
            _user = _userDAL.GetByID(userId);
            OnPropertyChanged(nameof(UserName));
            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Interests));
        }

        public string UserName => _user?.UserName ?? string.Empty;
        public string FirstName => _user?.FirstName ?? string.Empty;
        public string LastName => _user?.LastName ?? string.Empty;
        public string Email => _user?.Email ?? string.Empty;
        public string Interests => _user?.Interests != null ? string.Join(", ", _user.Interests) : string.Empty;


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
