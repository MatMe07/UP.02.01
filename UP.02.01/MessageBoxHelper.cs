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
        public static void MessInformation(string name)
        {
            MessageBox.Show(name, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public static void MessWarning(string name)
        {
            MessageBox.Show(name, "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        public static bool MessQuestion(string name)
        {
            var result = MessageBox.Show(name, "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }
        public static void MessError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
