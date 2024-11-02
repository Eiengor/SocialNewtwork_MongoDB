using DAL.Concreate;
using DTO;
using MongoDB.Bson;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WpfApp.Core;
using WpfApp.MVVM.Model;

namespace WpfApp.MVVM.ViewModel
{
    public class CommunityViewModel : ObservableObject, INotifyPropertyChanged
    {
        private DBConnect dbConnect = new DBConnect();
        private UserDAL _userDAL;
        private List<User> _users;
        private User _selectedUser;
        public RelayCommand UserInfoViewCommand { get; set; }
        public UserInfoViewModel UserInfoVM { get; set; }
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public List<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }
        public CommunityViewModel()
        {
            UserInfoVM = new UserInfoViewModel();
            _userDAL = new UserDAL(dbConnect.GetCollectionUsers());
            LoadUsers();

            UserInfoViewCommand = new RelayCommand( o => { CurrentView = UserInfoVM; });
        }
        private void LoadUsers()
        {
            Users = _userDAL.GetAll();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
