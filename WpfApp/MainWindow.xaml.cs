using DAL.Concreate;
using DTO;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System.IO;
using System.Windows;
using WpfApp.MVVM.Model;
using WpfApp.MVVM.View;
using WpfApp.MVVM.ViewModel;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DBConnect dbConnect = new DBConnect();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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