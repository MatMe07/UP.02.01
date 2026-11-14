using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Логика взаимодействия для CatalogProductsPage.xaml
    /// </summary>
    
    public partial class CatalogProductsPage : Page, INotifyPropertyChanged
    {
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        private List<Product> _allProducts;
        //public List<string>
        private bool IsAuth;
        public CatalogProductsPage(bool isAuth)
        {
            DataContext = this;
            InitializeComponent();
            IsAuth = isAuth; 
            if (!IsAuth)
            {
                stckPanelPoisk.Visibility = Visibility.Visible;
            }
            else
            {
                stckPanelPoisk.Visibility = Visibility.Hidden;
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _allProducts = Core.context.Products.ToList();
            Products.Clear();
            foreach (Product product in _allProducts)
            {
                Products.Add(product);
            }
        }
    }
}
