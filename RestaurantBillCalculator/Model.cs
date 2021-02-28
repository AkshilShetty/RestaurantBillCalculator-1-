using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RestaurantBillCalculator
{
    public class MenuItem
    {
        public MenuItem(string name, double price)
        {
            ItemName = name;
            ItemPrice = price;
        }

        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
    }


    public class OrderedItem : MenuItem, INotifyPropertyChanged
    {
        public OrderedItem(string name, double price) : base(name, price)
        {
            Quantity = 1;
        }

        private int quantity;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
                RaisePropertyChanged(nameof(Quantity));
            }
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public class Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Model()
        {
            Total = Tax = SubTotal = 0.0;
            Beverages = new ObservableCollection<MenuItem>()
{
new MenuItem("Soda", 1.95),
new MenuItem("Tea", 1.50),
new MenuItem("Coffee", 1.25),
new MenuItem("Mineral Water", 2.95),
new MenuItem("Juice", 2.50),
new MenuItem("Milk", 1.50)
};

            MainCourse = new ObservableCollection<MenuItem>()
{
new MenuItem("Chicken Alfredo", 13.95),
new MenuItem("Chicken Picatta", 13.95),
new MenuItem("Turkey Club", 11.95),
new MenuItem("Lobster Pie", 19.95),
new MenuItem("Prime Rib", 20.95),
new MenuItem("Shrimp Scampi", 18.95),
new MenuItem("Turkey Dinner", 13.95),
new MenuItem("Stuffed Chicken", 14.95)
};

            Appetizer = new ObservableCollection<MenuItem>()
{
new MenuItem("Buffalo Wings", 5.95),
new MenuItem("Buffalo Fingers", 6.95),
new MenuItem("Potato Skins", 8.95),
new MenuItem("Nachos", 8.95),
new MenuItem("Mushroom Caps", 10.95),
new MenuItem("Chips and Salsa", 6.95)
};

            Dessert = new ObservableCollection<MenuItem>()
{
new MenuItem("Apple Pie", 5.95),
new MenuItem("Sundae", 3.95),
new MenuItem("Carrot Cake", 5.95),
new MenuItem("Mud Pie", 4.95),
new MenuItem("Apple Crisp", 5.95)
};

            Order = new ObservableCollection<OrderedItem>();
        }

        public ObservableCollection<MenuItem> Beverages { get; set; }

        public ObservableCollection<MenuItem> MainCourse { get; set; }

        public ObservableCollection<MenuItem> Appetizer { get; set; }

        public ObservableCollection<MenuItem> Dessert { get; set; }

        public ObservableCollection<OrderedItem> Order { get; set; }

        private double total;

        public double Total
        {
            get { return total; }
            set
            {
                total = value;
                RaisePropertyChanged(nameof(Total));
            }
        }

        private double tax;

        public double Tax
        {
            get { return tax; }
            set
            {
                tax = value;
                RaisePropertyChanged(nameof(Tax));
            }
        }

        private double subTotal;

        public double SubTotal
        {
            get { return subTotal; }
            set
            {
                subTotal = value;
                RaisePropertyChanged(nameof(SubTotal));
            }
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RecalculateBill()
        {
            Total = Tax = SubTotal = 0.0;
            foreach (var item in Order)
            {
                Total += item.ItemPrice * item.Quantity;
            }
            Tax = Total * 0.10; //Calculating tax @ 10%. Change accordingly
            SubTotal = Total + Tax;
        }

        public void ClearBill()
        {
            Total = Tax = SubTotal = 0.0;
        }
    }
}

