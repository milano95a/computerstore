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
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {
        List<string> categoryList = new List<string>();
        Controller controller = Controller.ControllerInstance;

        public AddItem()
        {
            InitializeComponent();

            categoryList.Add("computer");
            categoryList.Add("accessoires");
            categoryList.Add("software");

            cbxCategory.DataContext = categoryList;

            txtType.GotFocus += TxtType_GotFocus;
            txtBrand.GotFocus += TxtBrand_GotFocus;
            txtDescription.GotFocus += TxtDescription_GotFocus;
            txtCharacteristics.GotFocus += TxtCharacteristics_GotFocus;
            txtPriceIn.GotFocus += TxtPriceIn_GotFocus;
            txtPriceOut.GotFocus += TxtPriceOut_GotFocus;       
            
            if(controller.currentItem != null)
            {
                Editable();                
            }              
        }

        private void TxtPriceOut_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPriceOut.Clear();
        }
        private void TxtPriceIn_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPriceIn.Clear();
        }
        private void TxtCharacteristics_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCharacteristics.Clear();
        }
        private void TxtDescription_GotFocus(object sender, RoutedEventArgs e)
        {
            txtDescription.Clear();
        }
        private void TxtBrand_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBrand.Clear();
        }
        private void TxtType_GotFocus(object sender, RoutedEventArgs e)
        {
            txtType.Clear();   
        }
        private void OnAddClicked(object sender, RoutedEventArgs e)
        {

            try
            {

                string category = categoryList[cbxCategory.SelectedIndex];
                string type = txtType.Text;
                string brand = txtBrand.Text;
                string description = txtDescription.Text;
                string characteristics = txtCharacteristics.Text;
                float priceIn = (float)Convert.ToDouble(txtPriceIn.Text);
                float priceOut = (float)Convert.ToDouble(txtPriceOut.Text);

                if (Controller.ControllerInstance.currentItem != null)
                {
                    controller.currentItem.Category = category;
                    controller.currentItem.Type = type;
                    controller.currentItem.Brand = brand;
                    controller.currentItem.Description = description;
                    controller.currentItem.Characteristics = characteristics;
                    controller.currentItem.PriceIn = priceIn;
                    controller.currentItem.PriceOut = priceOut;

                    controller.UpdateItem();
                    controller.currentItem = null;
                    MessageBox.Show("Item Updated");
                }
                else
                {
                    Item item = new Item(10, category, type, brand, description, characteristics, priceIn, priceOut);
                    controller.InsertItem(item);
                    MessageBox.Show("Item added");
                }

                new AllItemWindow().Show();
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please fill all fieds");
            }

        }
        private void Back(object sender, RoutedEventArgs e)
        {
            Controller.ControllerInstance.currentItem = null;

            var main = new MainWindow();
            main.Show();
            Close();
        }
        private void Editable()
        {
            cbxCategory.SelectedValue = controller.currentItem.Category;
            txtType.Text = controller.currentItem.Type;
            txtBrand.Text = controller.currentItem.Brand;
            txtDescription.Text = controller.currentItem.Description;
            txtCharacteristics.Text = controller.currentItem.Characteristics;
            txtPriceIn.Text = controller.currentItem.PriceIn + "";
            txtPriceOut.Text = controller.currentItem.PriceIn + "";            
        }
    }
}
