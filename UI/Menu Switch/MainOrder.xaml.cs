// Simon Moyal 1177707
// David Katz 065970394
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace UI.Menu_Switch
{
    /// <summary>
    /// Logique d'interaction pour MainOrder.xaml
    /// </summary>
    public partial class MainOrder : UserControl, iSwitchable
    {
        BL.IBL bl;
        /// <summary>
        /// Constructor , initialize data
        /// </summary>
        public MainOrder()
        {
            InitializeComponent();
            bl = BL.Factory_BL.GetBL();
            if (bl.ListOfOrder().Count() == 0)
            {
                DeleteButton.IsEnabled = false;
            }
            if (bl.ListOfOrdered().Count() == 0)
                UpdateButton.IsEnabled = false;
        }

        #region Switch's Function
        /// <summary>
        /// checking if we could switch to this window
        /// </summary>
        /// <param name="state"></param>
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Begin the order proccedure by getting first information about the client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            // switching to the client window
            Switcher.Switch(new ClientWindow());
        }
        /// <summary>
        /// Getting back to the main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // switching to the main window
            Switcher.Switch(new MainWindow());
        }
        /// <summary>
        /// Delete an order window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // switching to the delete order window
            Switcher.Switch(new DeleteOrderWindow());
        }
        /// <summary>
        /// Update window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sur to update your order ?, this action is irreversible", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                // switching to the update order window
                Switcher.Switch(new UpdateMenuOrderWindow());
        }
        #endregion

        

    }
}
