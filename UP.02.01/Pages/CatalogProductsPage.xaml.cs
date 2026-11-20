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
using UP._02._01.Windows;

namespace UP._02._01.Pages
{
    /// <summary>
    /// Логика взаимодействия для CatalogProductsPage.xaml
    /// </summary>
    
    public partial class CatalogProductsPage : Page, INotifyPropertyChanged
    {
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        public List<string> SortingLst { get; set; } 
        public List<string> FiltringLst { get; set; } 
        private List<Product> _allProducts;
        //public List<string>
        private bool IsAuth;
        private string txtSearch = "";
        public string TxtSearch
        {
            get
            {
                return txtSearch;
            }
            set
            {
                txtSearch = value;
                Sorting();
                OnPropertyChanged(nameof(TxtSearch));
            }
        }
        private string sortText = "Все";
        public string SortText
        {
            get
            {
                return sortText;
            }
            set
            {
                sortText = value;
                Sorting();
                OnPropertyChanged(nameof(SortText));
            }
        }
        private string filtText = "Все диапазоны";
        public string FiltText
        {
            get
            {
                return filtText;
            }
            set
            {
                filtText = value;
                Sorting();
                OnPropertyChanged(nameof(FiltText));
            }
        }
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

        public void Sorting()
        {
            var searchTxt = TxtSearch.ToLower();
            var lstProduct = _allProducts.Where(p=>
            p.Name.ToLower().Contains(searchTxt) ||
            p.Deskription.ToLower().Contains(searchTxt) ||
            p.Supplier.Name.ToLower().Contains(searchTxt) ||
            p.Manufacturer.Name.ToLower().Contains(searchTxt) ||
            p.UnitOfMeasures.ToLower().Contains(searchTxt) ||
            p.ProductsCategory.Name.ToLower().Contains(searchTxt)
            );
            switch (SortText)
            {
                case "По возрастанию по количеству на складе":
                    lstProduct = lstProduct.OrderBy(p => p.Count);
                    break;
                case "По убыванию по количеству на складе" :
                    lstProduct = lstProduct.OrderByDescending(p => p.Count);

                    break;
                case "По возрастанию по цене" :
                    lstProduct = lstProduct.OrderBy(p => p.totalPrice);

                    break;
                case "По убыванию по цене" :
                    lstProduct = lstProduct.OrderByDescending(p => p.totalPrice);

                    break;
                case "По возрастанию по скидке" :
                    lstProduct = lstProduct.OrderBy(p => p.Discount);

                    break;
                case "По убыванию по скидке" :
                    lstProduct = lstProduct.OrderByDescending(p => p.Discount);

                    break;
            }

            switch (FiltText)
            {
                case "0-11,99%": 
                    lstProduct = lstProduct.Where(p => p.Discount >= 0 && p.Discount <=11.99);
                    break;
                case "12 - 18,99 %":
                    lstProduct = lstProduct.Where(p => p.Discount >= 12 && p.Discount <= 18.99);

                    break;
                case "19 % и более":
                    lstProduct = lstProduct.Where(p => p.Discount >= 19);

                    break;
            }


            Products.Clear();

            foreach (Product product in lstProduct)
            {
                Products.Add(product);
            }

        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged (string propertyName)
        {
            PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _allProducts = Core.context.Products.ToList();
            //Categories = Core.context.ProductsCategories.Select(c=>c.Name).ToList();
            //(по возрастанию и убыванию) по количеству на складе, цене,
            //действующей скидке.
            SortingLst = new List<string> { 
                "Все",
                "По возрастанию по количеству на складе",
                "По убыванию по количеству на складе",
                "По возрастанию по цене",
                "По убыванию по цене",
                "По возрастанию по скидке",
                "По убыванию по скидке",
            };
            comboSort.ItemsSource = SortingLst;

            FiltringLst = new List<string>
            {
                "Все диапазоны",
                "0-11,99%",
                "12 - 18,99 %",
                "19 % и более",

            };
            comboFilt.ItemsSource = FiltringLst;
            //0-11,99%, 12 - 18,99 %, 19 % и более.

            Products.Clear();
            foreach (Product product in _allProducts)
            {
                Products.Add(product);
            }
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Product product = (sender as ListItem).DataContext as Product;
            var window = new EditNewProductWindow(product);
            window.ShowDialog() ;
        }
    }
}
