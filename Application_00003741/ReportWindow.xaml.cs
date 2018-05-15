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
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        public ReportWindow()
        {
            InitializeComponent();
            List<Order> orders = Controller.ControllerInstance.GetAllProcessedOrders();
            dataGridReport.DataContext = orders;

            SetTotalQuantity(orders);
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            
            if (orderDateTimePicker1.SelectedDate == null)
            {
                MessageBox.Show("Please select date");
                return;
            }
            else if (orderDateTimePicker2.SelectedDate == null)
            {
                MessageBox.Show("Please select date");
                return;
            }

            DateTime from = (DateTime)orderDateTimePicker1.SelectedDate;
            DateTime to = (DateTime)orderDateTimePicker2.SelectedDate;

            List<Order> orders = Controller.ControllerInstance.GenerateReport(from, to);

            dataGridReport.DataContext = orders;
            dataGridReport.Items.Refresh();

            SetTotalQuantity(orders);

        }

        private void Back(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        public void SetTotalQuantity(List<Order> orders)
        {
            float total = 0;
            int quantity = 0;

            foreach (Order o in orders)
            {
                foreach (OrderItem orderItem in Controller.ControllerInstance.GetOrderItemsById(o.Id))
                {
                    total += orderItem.Total;
                    quantity += orderItem.Quantity;
                }
            }
            txtQuantity.Text = "Quantity: " + quantity;

            txtTotal.Text = "Total: " + total;
        }
    }
}
