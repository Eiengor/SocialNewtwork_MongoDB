﻿using DAL.Concreate;
using DTO;
using MongoDB.Bson;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp.MVVM.Model;

namespace WpfApp.MVVM.ViewModel
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        public UserDAL _userDAL;
        public ObjectId _loggedInUserId;
        public User _user;
        //public List<Post> _userPosts;
        private DBConnect dbConnect = new DBConnect();
        private List<Post> _userPosts;
        public List<Post> UserPosts
        {
            get => _userPosts;
            set
            {
                _userPosts = value;
                OnPropertyChanged(nameof(UserPosts));
            }
        }
        public ProfileViewModel()
        {
            _userDAL = new UserDAL(dbConnect.GetCollectionUsers());
            _loggedInUserId = UserSession.LoggedInUserId;
            LoadUserData(_loggedInUserId);
            UserPosts = new UserPosts().GetPostsOfUser(_loggedInUserId); // Assign to property
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
