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
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Application_00003741
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AllOrders(object sender, RoutedEventArgs e)
        {
            var allOrder = new AllOrderWindow();
            allOrder.Show();
            Close();
        }

        private void AllItems(object sender, RoutedEventArgs e)
        {
            var allitems = new AllItemWindow();
            allitems.Show();
            Close();
        }

        private void OnAddItem(object sender, RoutedEventArgs e)
        {
            var addItem = new AddItem();
            addItem.Show();
            Close();
        }

        private void OnAddOrder(object sender, RoutedEventArgs e)
        {
            var addOrderWindow = new AddOrder();
            addOrderWindow.Show();
            Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void ViewReport(object sender, RoutedEventArgs e)
        {
            new ReportWindow().Show();
            Close();            
        }
    }
}