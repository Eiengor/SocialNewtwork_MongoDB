using DAL.Concreate;
using DTO;
using System.Windows;
using WpfApp.MVVM.Model;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public DBConnect dbConnect = new DBConnect();
        
        public User _currentUser;
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameBox.Text)) // check if the title is empty
            {
                MessageBox.Show("Please enter a user name!", "Wrong user name!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.IsNullOrEmpty(PasswordBox.Text))
            {
                MessageBox.Show("Please enter a password!", "Wrong password!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var _userDAL = new UserDAL(dbConnect.GetCollectionUsers());

                if (_userDAL.UserExists(UsernameBox.Text) && _userDAL.CorrectPassword(UsernameBox.Text, PasswordBox.Text))
                {
                    _currentUser = _userDAL.GetByUserName(UsernameBox.Text);
                    UserSession.SetCurrentUser(_currentUser.UserID);
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong name or password!", "Invalid Username or password", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
