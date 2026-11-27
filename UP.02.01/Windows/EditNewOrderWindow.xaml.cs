using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Migrations;
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
        public ObservableCollection<OrderStatus> orderStatuses { get; } = new ObservableCollection<OrderStatus>();
        public ObservableCollection<PickupPoint> PickupPoints { get; } = new ObservableCollection<PickupPoint>();
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

            var ordStatuses = Core.context.OrderStatuses;
            orderStatuses.Clear();
            foreach (OrderStatus status in ordStatuses)
            {
                orderStatuses.Add(status);
            }

            var punktVidachi = Core.context.PickupPoints;
            foreach (var item in punktVidachi)
            {
                PickupPoints.Add(item);
            }

            InitializeComponent();
            DataContext = this;

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Order.Equals(_OldOrder))
            {
                //this.Close();
                DialogResult = false;
                //Close();
            }
            if (MessageBoxHelper.MessQuestion("Есть несохраненные изменения.\n\nВы действительно хотите выйти?"))
            {
                Order.ID = _OldOrder.ID;
                Order.Articule = _OldOrder.Articule;
                Order.PickupPoint = _OldOrder.PickupPoint;
                Order.CodeOptaining = _OldOrder.CodeOptaining ;
                Order.OrderStatus = _OldOrder.OrderStatus;
                DialogResult = true;
            }
        }
        private bool CheckField()
        {
            if (string.IsNullOrWhiteSpace(Order.Articule) ||
                Order.PickupPoint == null ||
                Order.OrderStatus == null ||
                Order.CodeOptaining == 0 )
                return false;
            return true;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBoxHelper.MessQuestion("Вы дейстительно хотите удалить заказ?");
                if (result)
                {
                    Core.context.Orders.Remove(Order);
                    Core.context.SaveChanges();

                    MessageBoxHelper.MessInformation("Заказ успешно удален");
                    DialogResult = true;
                }
            }
            catch(Exception ex)
            {
                MessageBoxHelper.MessError($"Error: {ex}");
                return;
            }
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
                Core.context.Orders.AddOrUpdate(Order);
                Core.context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBoxHelper.MessError($"Error: {ex}");
                return;
            }

            MessageBoxHelper.MessInformation(!isNew ? "Все изменения успешно сохранены" : "Заказ успешно создан");
            DialogResult = true;
        }
    }
}
