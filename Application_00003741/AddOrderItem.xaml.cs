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
    /// <summary>
    /// Interaction logic for AddOrderItem.xaml
    /// </summary>
    public partial class AddOrderItem : Window
    {
        public AddOrderItem()
        {
            InitializeComponent();


            dataGridAddOrderItem.DataContext = Controller.ControllerInstance.GetAllItems();
            txtQuantity.GotFocus += TxtQuantity_GotFocus;
            txtQuantity.LostFocus += TxtQuantity_LostFocus;

            txtSellPrice.GotFocus += TxtSellPrice_GotFocus;
            txtSellPrice.LostFocus += TxtSellPrice_LostFocus;
        }
        private void TxtSellPrice_GotFocus(object sender, RoutedEventArgs e)
        {
            txtSellPrice.Clear();
        }
        private void TxtSellPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            string text = txtSellPrice.Text.Trim();

            if (text.Equals(""))
            {
                txtSellPrice.Text = "Selling Price";
            }
        }
        private void TxtQuantity_GotFocus(object sender, RoutedEventArgs e)
        {
            txtQuantity.Clear();
        }
        private void TxtQuantity_LostFocus(object sender, RoutedEventArgs e)
        {
            string text = txtQuantity.Text.Trim();

            if (text.Equals(""))
            {
                txtQuantity.Text = "Quantity";
            }
        }
        private void AddOrder(object sender, RoutedEventArgs e)
        {
            int index = dataGridAddOrderItem.SelectedIndex;
            Item item = null;

            int quantity;
            bool isQuantityNumber = int.TryParse(txtQuantity.Text.ToString(),out quantity);

            float sellingPrice;
            bool isSellingPrice = float.TryParse(txtSellPrice.Text.ToString(), out sellingPrice);

            if (index == -1)
            {
                MessageBox.Show("Please select an item");
                return;
            }
            else if(!isQuantityNumber)
            {
                MessageBox.Show("Please insert quantity");
                return;
            }else if(!isSellingPrice)
            {
                MessageBox.Show("Please insert selling price");
                return;
            }
            else
            {
                OrderItem orderItem = new OrderItem(Controller.ControllerInstance.items[index], quantity, sellingPrice);
                Controller.ControllerInstance.currentOrderItems.Add(orderItem);
                Close();
            }
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
} 