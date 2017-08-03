// Simon Moyal 1177707
// David Katz 065970394
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using BE;
using BL;

namespace UI.Menu_Switch
{
    /// <summary>
    /// Logique d'interaction pour AddDish.xaml
    /// </summary>
    public partial class AddDish : UserControl, iSwitchable
    {
        Dish dish;
        IBL bl;
        /// <summary>
        /// Constructor , initalize data
        /// </summary>
        public AddDish()
        {
            InitializeComponent();
            this.requestComboBox.ItemsSource = Enum.GetValues(typeof(CasheRoutLevel));
            this.sizeOfDishComboBox.ItemsSource = Enum.GetValues(typeof(size));
            dish = new Dish();
            this.DataContext = dish;
            bl = Factory_BL.GetBL();
        }
        #region Event
        /// <summary>
        /// Switching window with the information "Dsih"
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
        /// <summary>
        /// Add a dish button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // add dish
                bl.AddDish(dish);
                // reinitalize data , data binding
                dish = new Dish();
                this.DataContext = dish;
            }
            // in case we have an exception we catch it
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
        /// checking if we could switch to this window
        /// </summary>
        /// <param name="state"></param>
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
