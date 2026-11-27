using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace UP._02._01.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditNewOrderWindow.xaml
    /// </summary>
    public partial class EditNewOrderWindow : Window, INotifyPropertyChanged
    {

        private Order _OldOrder = new Order();
        private Order _order;
        public Order Order
        {
            get => _order;
            set
            {
                _order = value;
                OnPropertyChanged(nameof(Order));
            }
        }
        private bool isNew = false;
        public EditNewOrderWindow(Order order = null)
        {
            isNew = order == null;
            order = order ?? new Order();
            Order = order;

            _OldOrder.ID = order.ID;
            _OldOrder.Articule = order.Articule;
            _OldOrder.PickupPoint = order.PickupPoint;
            _OldOrder.CodeOptaining = order.CodeOptaining;
            _OldOrder.OrderStatus = order.OrderStatus;

            InitializeComponent();
            DataContext = this;

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
