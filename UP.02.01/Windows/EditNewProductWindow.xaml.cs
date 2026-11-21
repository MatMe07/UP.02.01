
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

        private Product origProduct;

        public ObservableCollection<ProductsCategory> productsCategories { get; } = new ObservableCollection<ProductsCategory>();
        public ObservableCollection<Supplier> Suppliers { get; } = new ObservableCollection<Supplier>();
        public ObservableCollection<Manufacturer> Manufactureres { get; } = new ObservableCollection<Manufacturer>();
        private bool isNew = true;
        public EditNewProductWindow(Product product = null)
        {

            origProduct = Clone(product);
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
                
                //Product.Articule = Product.Articule;
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
        public Product Clone(Product product)
        {
            var origProd = new Product();
            origProd.Name = product.Name;
            origProd.UnitOfMeasures = product.UnitOfMeasures;
            origProd.Prise = product.Prise;
            origProd.Supplier = product.Supplier;
            origProd.Manufacturer = product.Manufacturer;
            origProd.ProductsCategory = product.ProductsCategory;
            origProd.Discount = product.Discount;
            origProd.Count = product.Count;
            origProd.Deskription = product.Deskription;
            origProd.CoverPath = product.CoverPath;
            return origProd;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Product.Equals(origProduct))
            {
                //this.Close();
                DialogResult = false;
                Close();
            }
            if (MessageBox.Show("Есть несохраненные изменения", "Предупреждение", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Product.Name = origProduct.Name;
                Product.UnitOfMeasures = origProduct.UnitOfMeasures;
                Product.Prise= origProduct.Prise;
                Product.Supplier = origProduct.Supplier;
                Product.Manufacturer= origProduct.Manufacturer;
                Product.ProductsCategory= origProduct.ProductsCategory;
                Product.Discount= origProduct.Discount;
                Product.Count= origProduct.Count;
                Product.Deskription= origProduct.Deskription;
                Product.CoverPath= origProduct.CoverPath;
                DialogResult = true;

                this.Close();
            }

            //this.Close();
        }
    }
}
