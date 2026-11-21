
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UP._02._01.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditNewProductWindow.xaml
    /// </summary>
    public partial class EditNewProductWindow : Window, INotifyPropertyChanged
    {
        private Product _product;
        public Product Product
        {
            get { return _product; }
            set
            {
                _product = value;
                OnPropertyChanged(nameof(Product));
            }
        }

        public ObservableCollection<ProductsCategory> productsCategories { get; } = new ObservableCollection<ProductsCategory>();
        public ObservableCollection<Supplier> Suppliers { get; } = new ObservableCollection<Supplier>();
        public ObservableCollection<Manufacturer> Manufactureres { get; } = new ObservableCollection<Manufacturer>();
        private bool isNew = true;
        public EditNewProductWindow(Product product = null)
        {
            this.Product = product ?? new Product();
            isNew = product == null;
            InitializeComponent();
            DataContext = this;

            var categories = Core.context.ProductsCategories;

            productsCategories.Clear();
            foreach (ProductsCategory category in categories)
            {
                
                productsCategories.Add(category);
            }

            var suppliers = Core.context.Suppliers;
            Suppliers.Clear();
            foreach (Supplier supplier in suppliers)
            {
                Suppliers.Add(supplier);
            }


            var manufactures = Core.context.Manufacturers;
            Manufactureres.Clear();
            foreach (Manufacturer manufacturer in manufactures)
            {
                Manufactureres.Add(manufacturer);
            }


        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text[0]);
        }

        private void BtnChangeImage_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(Product.Manufacturer.Name);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSaveOrCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                Product.ID = Product.ID ?? "dfgsdfg";
                Core.context.Products.AddOrUpdate(Product);
                Core.context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void Window_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                txtPrice.Text = Product.Prise.ToString().Replace(',', '.');
                MessageBox.Show("Неверный формат");
            }
        }
    }
}
