using DAL.Concreate;
using DAL_Neo4j.Concreate;
using DTO;
using MongoDB.Bson;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfApp.Core;
using WpfApp.MVVM.Model;

namespace WpfApp.MVVM.ViewModel
{
    public class UserInfoViewWindowModel : Window, INotifyPropertyChanged
    {
        public UserDAL _userDAL;
        public UserDAL_Neo4j _userDAL_Neo4j;
        public ObjectId _selectedUserId;
        public User _user;
        private DBConnect dbConnect = new DBConnect();
        private List<Post> _userPosts;
        public RelayCommand FollowCommand => new RelayCommand(execute => Follow(_selectedUserId, UserSession.LoggedInUserId));
        public RelayCommand UnfollowCommand => new RelayCommand(execute => Unfollow(_selectedUserId, UserSession.LoggedInUserId));
        public List<Post> UserPosts
        {
            get => _userPosts;
            set
            {
                _userPosts = value;
                OnPropertyChanged(nameof(UserPosts));
            }
        }
        private bool _isFollowing;
        public bool IsFollowing
        {
            get => _isFollowing;
            set
            {
                _isFollowing = value;
                OnPropertyChanged(nameof(IsFollowing));
            }
        }
        private string _relation;
        public string Relation
        {
            get => _relation;
            set
            {
                _relation = value;
                OnPropertyChanged(nameof(Relation));
            }
        }
        private string _numberOfFollower;
        public string NumberOfFollowers
        {
            get => _numberOfFollower;
            set
            {
                _numberOfFollower = value;
                OnPropertyChanged(nameof(NumberOfFollowers));
            }
        }

        public UserInfoViewWindowModel() { }

        public void Initialize(string username)
        {
            _userDAL = new UserDAL(dbConnect.GetCollectionUsers());
            _userDAL_Neo4j = dbConnect.GetNeo4JCollectionUsers();
            _selectedUserId = _userDAL.GetByUserName(username).UserID;
            LoadUserData(_selectedUserId);
            UserPosts = new UserPosts().GetPostsOfUser(_selectedUserId);
            IsFollowing = isFollow(_selectedUserId, UserSession.LoggedInUserId);
            Relation = CheckRelation(_selectedUserId, UserSession.LoggedInUserId);
            NumberOfFollowers = CheckNumberOfFollowers(_selectedUserId);
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

        async public void Follow(ObjectId userToFollowID, ObjectId LoggedInUserId)
        {
            _userDAL.AddFollower(userToFollowID, LoggedInUserId);
            _userDAL.AddFollowing(LoggedInUserId, userToFollowID);
            await _userDAL_Neo4j.AddRelation(_userDAL.GetByID(LoggedInUserId), userToFollowID);
        }
        async public void Unfollow(ObjectId userToUnfollowID, ObjectId LoggedInUserId)
        {
            _userDAL.DeleteFollower(userToUnfollowID, LoggedInUserId);
            _userDAL.DeleteFollowing(LoggedInUserId, userToUnfollowID);
            await _userDAL_Neo4j.DeleteRelation(_userDAL.GetByID(LoggedInUserId), userToUnfollowID);
        }
        public bool isFollow(ObjectId userToFollowID, ObjectId LoggedInUserId)
        {
            return _userDAL.GetByID(LoggedInUserId).FollowingIDs.Contains(userToFollowID);
        }
        public string CheckRelation(ObjectId selectedUserID, ObjectId LoggedInUserID)
        {
            if(_userDAL.GetByID(selectedUserID).FollowingIDs.Contains(LoggedInUserID))
            {
                return "This user follows you";
            }
            else
            {
                return "This user does not follow you";
            }
        }
        public string CheckNumberOfFollowers(ObjectId selectedUserID)
        {
            return _userDAL.GetByID(selectedUserID).FollowerIDs.Count().ToString();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
