
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.IO;
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
//using System.Windows.Shapes;

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
        private string pathDirectory;

        private string _coverPahtName;
        public string CoverPahtName
        {
            get => _coverPahtName;
            set
            {
                _coverPahtName = value;
                OnPropertyChanged(nameof(CoverPahtName));
            }
        }
        public EditNewProductWindow(Product product = null)
        {
            isNew = product == null;
            if (product == null) product = new Product();
            origProduct = Clone(product);
            this.Product = product ?? new Product();
            InitializeComponent();
            DataContext = this;

            btnDelete.Visibility = isNew ? Visibility.Collapsed : Visibility.Visible;

            pathDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            pathDirectory = $"{pathDirectory}\\images";
            CoverPahtName = Product.CoverPath;

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

        private void SaveImg()
        {
            var filePath = Product.CoverPath;

            var path = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            Console.WriteLine(path);
            var fileName = Path.GetFileName(filePath);
            File.Copy(filePath, $"{pathDirectory}\\{CoverPahtName}");
        }

        private void BtnChangeImage_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(Product.CoverPathImg);
            var res = new OpenFileDialog();
            res.Filter = "(*.png)|*.png|(*.jpg)|*.jpg";


            if (res.ShowDialog() == true)
            {
                var filePath = res.FileName;
                Product.CoverPath = filePath;
                CoverPahtName = Path.GetFileName(filePath);
                OnPropertyChanged(nameof(Product));
            }

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBoxHelper.MessQuestion("Вы дейстительно хотите удалить продукт?" );
            if (result)
            {
                
                Product.IsActive = false;
                MessageBoxHelper.MessInformation("Продукт успешно удален" );
                DialogResult = true;
            }

        }
        private bool CheckField()
        {
            if (string.IsNullOrWhiteSpace(Product.Name) || 
                string.IsNullOrWhiteSpace(Product.Deskription) || 
                string.IsNullOrWhiteSpace(Product.UnitOfMeasures) || 
                Product.ProductsCategory == null || 
                Product.Supplier == null || 
                Product.Manufacturer == null || 
                Product.Prise == 0 )
                return false;
            return true;
        }

        private void BtnSaveOrCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckField())
            {
                MessageBoxHelper.MessWarning("Есть незаполненные поля");
                return;
            }
            try
            {
                
                //Product.Articule = Product.Articule;
                Core.context.Products.AddOrUpdate(Product);
                Core.context.SaveChanges();
                if (Product.CoverPath != origProduct.CoverPath)
                    SaveImg();
            }
            catch (Exception ex)
            {
                MessageBoxHelper.MessError($"Error: {ex}");
                return;
            }
            MessageBoxHelper.MessInformation(!isNew ? "Все изменения успешно сохранены": "Продукт успешно создан");
            DialogResult = true;
            //Close();
        }

        private void Window_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                txtPrice.Text = Product.Prise.ToString().Replace(',', '.');
                MessageBoxHelper.MessError("Неверный формат");
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
                //Close();
            }
            if (MessageBoxHelper.MessQuestion("Есть несохраненные изменения.\n\nВы действительно хотите выйти?"))
            {
                Product.CoverPath = null;
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

                //this.Close();
            }

            //this.Close();
        }
    }
}
