// Simon Moyal 1177707
// David Katz 065970394
using System;
using System.Windows;
using System.Windows.Controls;
using BE;
using BL;

namespace UI.Menu_Switch
{
    /// <summary>
    /// Logique d'interaction pour DeleteDish.xaml
    /// </summary>
    public partial class DeleteDish : UserControl, iSwitchable
    {
        Dish dish;
        IBL bl;
        /// <summary>
        /// Constructor , initialize data
        /// </summary>
        public DeleteDish()
        {
            InitializeComponent();
            dish = new Dish();
            bl = Factory_BL.GetBL();
            this.ListCombobox.ItemsSource = bl.ListOfDish();
            this.CancelButton.IsEnabled = false;
        }
        
        #region Event
        /// <summary>
        /// Checking the value select by the user in order to grab information and unlock button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListCombobox.SelectedValue != null)
            {
                dish = (Dish)ListCombobox.SelectedValue;
                CancelButton.IsEnabled = true;
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
        /// <sumanmary>
        /// Delete a dish
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // asking confirmation from the user
            if (MessageBox.Show("Are you sur to delete your order ? this action is irreversible", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    // deleteing the dish ( all the data already contain in the dish object thank to data binding) 
                    bl.DeleteDish(dish.MenuID);
                    // refreshing the list
                    this.ListCombobox.ItemsSource = bl.ListOfDish(x => x.MenuID != dish.MenuID);
                    dish = new Dish();
                    CancelButton.IsEnabled = false;
                }
                // if we have an exception
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// Switching back to the operation window with the information "Dish"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Operation("Dish"));
        }
        #endregion
    }
}
