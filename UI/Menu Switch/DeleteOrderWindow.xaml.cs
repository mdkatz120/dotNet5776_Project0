// Simon Moyal 1177707
// David Katz 065970394
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BE;
using BL;

namespace UI.Menu_Switch
{
    /// <summary>
    /// Logique d'interaction pour DeleteOrderWindow.xaml
    /// </summary>
    public partial class DeleteOrderWindow : UserControl, iSwitchable
    {
        // declaration of useful data
        Order order;
        BL.IBL bl;
        /// <summary>
        /// Constructor, initialize all data
        /// </summary>
        public DeleteOrderWindow()
        {
            InitializeComponent();
            order = new Order();
            // getting all the function
            bl = Factory_BL.GetBL();
            this.ListCombobox.ItemsSource = bl.ListOfOrder();
            this.CancelButton.IsEnabled = false;
        }

        #region Event
        /// <summary>
        /// // switching back to main order window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainOrder());
        }
        /// <summary>
        /// Switching back to the previous window
        /// </summary>
        /// <param name="state"></param>
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Checking the value of the combobox in order to unlock button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListCombobox.SelectedValue != null)
                this.CancelButton.IsEnabled = true;
            else
                this.CancelButton.IsEnabled = false;
        }
        /// <summary>
        /// Cancel button , canceling an order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Asking confirmation from the user
            if (MessageBox.Show("Are you sur to delete your order ? this action is irreversible", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    order = bl.ListOfOrder(o => o.OrderNumber == ((Order)ListCombobox.SelectedValue).OrderNumber).FirstOrDefault();
                    // deleting all the ordered dish and the order himself
                    bl.DeleteAllOrdered(order.OrderNumber);
                    MessageBox.Show("The order has been succefully delete");
                    // updating the list
                    this.ListCombobox.ItemsSource = bl.ListOfOrder(x => x.OrderNumber != order.OrderNumber);
                    // reinitialize other value
                    this.DataContext = order;
                    order = new Order();
                }
                // an excpeption has been occure
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion
    }
}
