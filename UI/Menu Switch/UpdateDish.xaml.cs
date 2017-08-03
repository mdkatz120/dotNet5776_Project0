// Simon Moyal 1177707
// David Katz 065970394
using System;
using System.Windows;
using System.Windows.Controls;
using BE;
using BL;
using System.Windows.Media.Imaging;

namespace UI.Menu_Switch
{
    /// <summary>
    /// Logique d'interaction pour UpdateDish.xaml
    /// </summary>
    public partial class UpdateDish : UserControl, iSwitchable
    {
        Dish dish;
        IBL bl;
        /// <summary>
        /// Constructor , initialize data
        /// </summary>
        public UpdateDish()
        {
            InitializeComponent();
            dish = new Dish();
            this.DataContext = dish;
            bl = Factory_BL.GetBL();
            ListBranch.ItemsSource = bl.ListOfDish();
            nameTextBox.IsEnabled = preparationTimeTextBox.IsEnabled = priceOfDishTextBox.IsEnabled = quantityTextBox.IsEnabled = requestComboBox.IsEnabled = sizeOfDishComboBox.IsEnabled = false;
            this.requestComboBox.ItemsSource = Enum.GetValues(typeof(BE.CasheRoutLevel));
            this.sizeOfDishComboBox.ItemsSource = Enum.GetValues(typeof(size));
        }

        #region Event
        /// <summary>
        /// checking if we could switch to this window
        /// </summary>
        /// <param name="state"></param>
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Update dish
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // update function
                bl.UpdateDish(((Dish)ListBranch.SelectedValue).MenuID, dish);
                // refrshing the list
                ListBranch.ItemsSource = bl.ListOfDish();
                // confirmation
                MessageBox.Show("Dish succefully update");
                // reinitialize data
                dish = new Dish();
                this.DataContext = dish;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Checking the value of the combobox in order to unlock button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBranch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBranch.SelectedValue != null)
            {
                nameTextBox.IsEnabled = preparationTimeTextBox.IsEnabled = priceOfDishTextBox.IsEnabled = quantityTextBox.IsEnabled = requestComboBox.IsEnabled = sizeOfDishComboBox.IsEnabled = true;
            }
        }
        /// <summary>
        /// Checking the value of the textbox, we sould receive a number not a single letter is allowed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void priceOfDishTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if we have some trouble to convert the text of the textbox it means that we have non number character which is forbidden
            int tmp;
            if (!int.TryParse(priceOfDishTextBox.Text, out tmp))
            {
                priceOfDishTextBox.Text = "";
            }
        }
        /// <summary>
        /// Checking the value of the textbox, we sould receive a string not a number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if we succefully convert the text of the textbox it means that we only have number which is forbidden
            int tmp;
            if (int.TryParse(nameTextBox.Text, out tmp))
            {
                nameTextBox.Text = "";
            }
        }
        /// <summary>
        /// Checking the value of the textbox, we sould receive a number not a single letter is allowed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preparationTimeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if we have some trouble to convert the text of the textbox it means that we have non number character which is forbidden
            int tmp;
            if (!int.TryParse(preparationTimeTextBox.Text, out tmp))
            {
                preparationTimeTextBox.Text = "";
            }
        }
        /// <summary>
        /// Checking the value of the textbox, we sould receive a number not a single letter is allowed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quantityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if we have some trouble to convert the text of the textbox it means that we have non number character which is forbidden
            int tmp;
            if (!int.TryParse(quantityTextBox.Text, out tmp))
            {
                quantityTextBox.Text = "";
            }
        }
        /// <summary>
        /// Switching back to the operation window with the information "Dish"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Operation("Dish"));
        }
        /// <summary>
        /// Button to choose or change an image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Change_Click(object sender, RoutedEventArgs e)
        {
            // open a window to browse an image on your system
            Microsoft.Win32.OpenFileDialog f = new Microsoft.Win32.OpenFileDialog();
            // list of extension allowed
            f.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            // if we have confirmation , add the image
            if (f.ShowDialog() == true)
            {
                this.Image.Source = new BitmapImage(new Uri(f.FileName));
            }
        }
        #endregion


    }
}
