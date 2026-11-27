using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using UP._02._01.Windows;

namespace UP._02._01.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public ObservableCollection<Order> Orders { get;  } = new ObservableCollection<Order>();
        private List<Order> _AllOrders;
        public OrderPage() 
        {
            InitializeComponent();
            DataContext = this;
            RefreshData();
        }

        public void RefreshData()
        {
            _AllOrders = Core.context.Orders.ToList();
            Orders.Clear();
            foreach (var item in _AllOrders)
            {
                Orders.Add(item);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Order order = (sender as ListBox).SelectedItem as Order;

            var window = new EditNewOrderWindow(order);
            var rShd = window.ShowDialog();
            if (rShd.Value)
            {

                RefreshData();
            }
        }

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            var window = new EditNewOrderWindow();
            var rShd = window.ShowDialog();
            if (rShd.Value)
            {

                RefreshData();
            }
        }
    }
}
