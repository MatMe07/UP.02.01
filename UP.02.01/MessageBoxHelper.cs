using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UP._02._01
{
    public class MessageBoxHelper
    {
        public static void MessInformation(string name, string message)
        {
            MessageBox.Show(name, message, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public static void MessError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
