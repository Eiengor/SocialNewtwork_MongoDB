using DAL.Concreate;
using DTO;
using System.Windows;
using System.Windows.Controls;
using WpfApp.MVVM.Model;
using WpfApp.MVVM.ViewModel;

namespace WpfApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for CommunityView.xaml
    /// </summary>
    public partial class CommunityView : UserControl
    {
        public DBConnect dbConnect = new DBConnect();
        public CommunityView()
        {
            InitializeComponent();
        }
        private void GoToUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameBox.Text))
            {
                MessageBox.Show("Please enter a user name!", "Wrong user name!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var _userDAL = new UserDAL(dbConnect.GetCollectionUsers());
                if (_userDAL.UserExists(UsernameBox.Text))
                {
                    UserInfoViewWindow userInfoVW = new UserInfoViewWindow(UsernameBox.Text);
                    userInfoVW.Show();
                }
                else
                {
                    MessageBox.Show("Wrong name!", "Invalid Username", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }
    }
}
