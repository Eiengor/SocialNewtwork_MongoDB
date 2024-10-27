using DAL.Concreate;
using DTO;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace WpfApp.MVVM.ViewModel
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        public UserDAL _userDAL;
        public ObjectId _loggedInUserId;
        public User _user;

        public ProfileViewModel()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                .Build();

            string connectionString = config.GetConnectionString("DbConnection") ?? "";

            IMongoClient client = new MongoClient(connectionString);
            IMongoDatabase db = client.GetDatabase("SocialNetwork");

            var users = db.GetCollection<User>("users");
            _userDAL = new UserDAL(users);
            _loggedInUserId = new ObjectId("670e88f54f32237ee312c59a");
            LoadUserData(_loggedInUserId);
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
