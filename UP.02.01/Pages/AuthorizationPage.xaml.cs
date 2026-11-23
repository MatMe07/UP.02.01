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

namespace UP._02._01.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CatalogProductsPage(false));
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text)
                || string.IsNullOrEmpty(txtPassword.Password))
            {
                MessageBoxHelper.MessError("Заполните все поля!");
                return;
            }
            var user = Core.context.Users.FirstOrDefault(u=>u.Login ==  txtLogin.Text && u.Password == txtPassword.Password);
            if (user == null)
            {
                MessageBoxHelper.MessError("Нет такого пользователя");
                return;
            }
            MessageBoxHelper.MessInformation("Вы успешно вошли в систему");
            UserData.User = user;
            NavigationService.Navigate(new CatalogProductsPage(false));

        }
    }
}
