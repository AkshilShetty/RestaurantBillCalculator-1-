using System;
using System.Collections.Generic;
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

namespace RestaurantBillCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new Model();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dataContext = DataContext as Model;
            var item = e.AddedItems[0] as MenuItem;
            if (dataContext == null || item == null)
                return;

            if (dataContext.Order.Any(o => o.ItemName == item.ItemName))
            {
                var orderItem = dataContext.Order.First(o => o.ItemName == item.ItemName);
                orderItem.Quantity++;
            }
            else
            {
                dataContext.Order.Add(new OrderedItem(item.ItemName, item.ItemPrice));
            }

            dataContext.RecalculateBill();
        }

       

        private void MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.centennialcollege.ca");
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var dataContext = DataContext as Model;
            dataContext.ClearBill();
        }
    }
}
