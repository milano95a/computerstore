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
using System.Windows.Shapes;
using Library_00003741;

namespace Application_00003741
{
    public partial class AllOrderWindow : Window
    {      
        public AllOrderWindow()
        {
            InitializeComponent();      
                  
            dataGridOrderAll.DataContext = Controller.ControllerInstance.GetAllOrders();
            radiobtnAll.IsChecked = true;
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
        private void AllOrders(object sender, RoutedEventArgs e)
        {
            dataGridOrderAll.DataContext = Controller.ControllerInstance.GetAllOrders();
            dataGridOrderAll.Items.Refresh();
        }
        private void Open(object sender, RoutedEventArgs e)
        {
            dataGridOrderAll.DataContext = Controller.ControllerInstance.GetAllOpenOrders();
            dataGridOrderAll.Items.Refresh();
        }
        private void Processed(object sender, RoutedEventArgs e)
        {
            dataGridOrderAll.DataContext = Controller.ControllerInstance.GetAllProcessedOrders();
            dataGridOrderAll.Items.Refresh();

        }
        private void Cancelled(object sender, RoutedEventArgs e)
        {
            dataGridOrderAll.DataContext = Controller.ControllerInstance.GetAllCancelled();
            dataGridOrderAll.Items.Refresh();

        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            int index = dataGridOrderAll.SelectedIndex;

            if (index == -1)
            {
                MessageBox.Show("Please select order");
                return;
            }

            Controller.ControllerInstance.currentOrder = Controller.ControllerInstance.allOrders[index];
            Controller.ControllerInstance.DeleteOrder();

            Refresh();           
        }
        private void Edit(object sender, RoutedEventArgs e)
        {
            int index = dataGridOrderAll.SelectedIndex;

            if(index == -1)
            {
                MessageBox.Show("Please select order");
                return;
            }

            Controller con = Controller.ControllerInstance;
                                   
            con.currentOrder = con.allOrders[index];
            con.currentOrderItems = con.GetOrderItemsById(con.currentOrder.Id);            
            con.editable = true;
            con.DeleteOrder();

            AddOrder addOrderWindow = new AddOrder();
            addOrderWindow.Show();
            Close();
        }
        private void Refresh()
        {            
            dataGridOrderAll.Items.Refresh();
        }
        private void Newest(object sender, RoutedEventArgs e)
        {
            List<Order> a = Controller.ControllerInstance.allOrders;

            List<Order> SortedList = a.OrderBy(o => o.OrderDate).ToList();
            dataGridOrderAll.DataContext = SortedList;
            Controller.ControllerInstance.allOrders = SortedList;
            dataGridOrderAll.Items.Refresh();
        }
        private void Oldest(object sender, RoutedEventArgs e)
        {
            List<Order> a = Controller.ControllerInstance.allOrders;

            List<Order> SortedList = a.OrderByDescending(o => o.OrderDate).ToList();
            dataGridOrderAll.DataContext = SortedList;
            Controller.ControllerInstance.allOrders = SortedList;
            dataGridOrderAll.Items.Refresh();
        }
    }
}