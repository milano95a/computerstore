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
using System.ComponentModel;

namespace Application_00003741
{
    /// <summary>
    /// Interaction logic for AddOrder.xaml
    /// </summary>
        
    public partial class AddOrder : Window
    {
        List<string> statusList = new List<string>();
        public List<OrderItem> orderItems = new List<OrderItem>();
      
        public AddOrder()
        {
            InitializeComponent();

            txtAddress.GotFocus += TxtAddress_GotFocus;
            txtAddress.LostFocus += TxtAddress_LostFocus;

            statusList.Add("open");
            statusList.Add("processed");
            statusList.Add("cancelled");

            if (Controller.ControllerInstance.editable)
            {
                Editable();
            }
            
            dataGridAddOrder.DataContext = Controller.ControllerInstance.currentOrderItems;
            cbxStatus.DataContext = statusList;

            float total = 0;

            foreach (OrderItem orderItem in Controller.ControllerInstance.currentOrderItems)
            {
                total += orderItem.Total;
            }

            txtTotal.Text = total + "";
        }
        private void TxtAddress_GotFocus(object sender, RoutedEventArgs e)
        {
            txtAddress.Clear();
        }
        private void TxtAddress_LostFocus(object sender, RoutedEventArgs e)
        {
            string text = txtAddress.Text.Trim();

            if (text.Equals(""))
            {
                txtAddress.Text = "Delivery Address";
            }
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("want to save changes", "Order", MessageBoxButton.YesNoCancel);

            switch (res)
            {
                case MessageBoxResult.Yes:
                    SaveAll();
                    break;

                case MessageBoxResult.No:
                    new AllOrderWindow().Show();
                    this.Close();
                    break;
                case MessageBoxResult.Cancel:

                    break;
            }
        }                
        private void OnAddItem(object sender, RoutedEventArgs e)
        {
            var addOrderItem = new AddOrderItem();
            addOrderItem.Show();

            addOrderItem.Closing += AddWindowsClosedHandler;            
        }
        private void AddWindowsClosedHandler (object sender,  EventArgs e)
        {
            Refresh();


        }
        private void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            SaveAll();
        }
        private void Refresh(object sender, RoutedEventArgs e)
        {
            Refresh();

        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            int index = dataGridAddOrder.SelectedIndex;

            if (index == -1)
            {
                MessageBox.Show("Please select an item");
                return;
            }

            Controller.ControllerInstance.currentOrderItems.RemoveAt(index);

            Refresh();

            MessageBox.Show("Deleted");

        }
        public void Refresh()
        {
            dataGridAddOrder.Items.Refresh();
            float total = 0;

            foreach (OrderItem orderItem in Controller.ControllerInstance.currentOrderItems)
            {
                total += orderItem.Total;
            }

            txtTotal.Text = total + "";
        }
        private void Editable()
        {
            txtAddress.Text = Controller.ControllerInstance.currentOrder.Address;
            txtTotal.Text = Controller.ControllerInstance.currentOrder.Total + "";
            cbxStatus.SelectedValue = Controller.ControllerInstance.currentOrder.Status;
            orderDateTimePicker.SelectedDate = Controller.ControllerInstance.currentOrder.OrderDate;
        }
        private void SaveAll()
        {
            string address = txtAddress.Text;
            int index = cbxStatus.SelectedIndex;

            if (orderDateTimePicker.SelectedDate == null)
            {
                MessageBox.Show("Please select order date");
                return;
            }
            if (txtAddress.Text.Equals("Delivery Address"))
            {
                MessageBox.Show("Please insert address");
                return;
            }
            if (index == -1)
            {
                MessageBox.Show("Please select order status");
                return;
            }
            if (Controller.ControllerInstance.currentOrderItems.Count < 1)
            {
                MessageBox.Show("There are no items in order");
                return;
            }

            DateTime orderDate = (DateTime)orderDateTimePicker.SelectedDate;

            Controller.ControllerInstance.InserOrder(new Order(orderDate, address, statusList[index]));

            if (Controller.ControllerInstance.editable)
            {
                Controller.ControllerInstance.editable = false;
                MessageBox.Show("Order Updated");
            }
            else
            {
                MessageBox.Show("Order Saved");
            }

            AllOrderWindow allOrderWindow = new AllOrderWindow();
            allOrderWindow.Show();
            this.Close();
        }
    }
}