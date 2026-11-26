using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UP._02._01
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            if (UserData.IsAuth)
            {
                
                //btnOrder.Visibility =  UserData.User.IsAdmin ? Visibility.Visible : Visibility.Collapsed;
                txtUserName.Visibility = Visibility.Visible;
                txtUserName.Text = UserData.User.FIO;
            }
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
