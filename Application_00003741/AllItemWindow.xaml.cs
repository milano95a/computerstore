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

namespace Application_00003741
{
    /// <summary>
    /// Interaction logic for AllItemWindow.xaml
    /// </summary>
    public partial class AllItemWindow : Window
    {
        Controller controller;

        public AllItemWindow()
        {
            InitializeComponent();

            SetUp();
        }

        public void SetUp()
        {
            controller = Controller.ControllerInstance;
            dataGridItemAll.DataContext = controller.GetAllItems();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            var mainWindows = new MainWindow();
            mainWindows.Show();
            Close();
        }

        private void OnAll(object sender, RoutedEventArgs e)
        {
            All.Visibility = Visibility.Visible;
            Computer.Visibility = Visibility.Collapsed;
            Accessoires.Visibility = Visibility.Collapsed;
            Software.Visibility = Visibility.Collapsed;

            dataGridItemAll.DataContext = controller.GetAllItems();
            dataGridItemAll.Items.Refresh();
        }
        private void OnComputer(object sender, RoutedEventArgs e)
        {
            All.Visibility = Visibility.Collapsed;
            Computer.Visibility = Visibility.Visible;
            Accessoires.Visibility = Visibility.Collapsed;
            Software.Visibility = Visibility.Collapsed;


            dataGridItemAll.DataContext = controller.GetAllComputers();
            dataGridItemAll.Items.Refresh();

        }
        private void OnAccessoires(object sender, RoutedEventArgs e)
        {
            All.Visibility = Visibility.Collapsed;
            Computer.Visibility = Visibility.Collapsed;
            Accessoires.Visibility = Visibility.Visible;
            Software.Visibility = Visibility.Collapsed;

            dataGridItemAll.DataContext = controller.GetAllAccessoires();
            dataGridItemAll.Items.Refresh();

        }
        private void OnSoftware(object sender, RoutedEventArgs e)
        {
            All.Visibility = Visibility.Collapsed;
            Computer.Visibility = Visibility.Collapsed;
            Accessoires.Visibility = Visibility.Collapsed;
            Software.Visibility = Visibility.Visible;

            dataGridItemAll.DataContext = controller.GetAllSoftware();
            dataGridItemAll.Items.Refresh();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            int index = dataGridItemAll.SelectedIndex;

            if (index == -1)
            {
                MessageBox.Show("Please select order");
                return;
            }

            Controller con = Controller.ControllerInstance;

            con.currentItem = con.items[index];

            new AddItem().Show();
            Close();
        }
    }
}
