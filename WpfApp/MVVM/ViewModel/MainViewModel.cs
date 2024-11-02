using MongoDB.Bson;
using WpfApp.Core;

namespace WpfApp.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand CommunityViewCommand { get; set; }
        public RelayCommand ProfileViewCommand { get; set; }
        //public RelayCommand UserInfoViewCommand { get; set; }
        public RelayCommand AddPostViewCommand { get; set; }
        //public UserInfoViewModel UserInfoVM { get; set; }
        public HomeViewModel HomeVM { get; set; }
        public CommunityViewModel CommunityVM { get; set; }
        public ProfileViewModel ProfileVM { get; set; }
        public AddPostViewModel AddPostVM { get; set; }

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

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            CommunityVM = new CommunityViewModel();
            ProfileVM = new ProfileViewModel();
            AddPostVM = new AddPostViewModel();
            //UserInfoVM = new UserInfoViewModel();
            CurrentView = HomeVM;

            //UserInfoViewCommand = new RelayCommand(o => { CurrentView = ProfileVM; });
            HomeViewCommand = new RelayCommand(o => { CurrentView = HomeVM; });
            CommunityViewCommand = new RelayCommand(o => { CurrentView = CommunityVM; });
            ProfileViewCommand = new RelayCommand(o => { CurrentView = ProfileVM; });
            AddPostViewCommand = new RelayCommand(o => { CurrentView = AddPostVM; });
        }
    }
}
