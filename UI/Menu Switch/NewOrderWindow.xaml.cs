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
    /// Logique d'interaction pour NewOrderWindow.xaml
    /// </summary>
    public partial class NewOrderWindow : UserControl, iSwitchable
    {
        Order order;
        IBL bl;
        /// <summary>
        /// Constructor , initailize data
        /// </summary>
        public NewOrderWindow()
        {
            InitializeComponent();
            // create new order
            order = new BE.Order();
            // finalize binding
            this.DataContext = order;
            // getting all function
            bl = BL.Factory_BL.GetBL();
            // loading the combobox with the value of enum
            this.cashroutLevelComboBox.ItemsSource = Enum.GetValues(typeof(BE.CasheRoutLevel));
            // loading combobox list with function calling
            this.ComboboxList.ItemsSource = bl.ListOfBranch();
            // setting button off until all wh have unfill field
            this.ComboboxList.IsEnabled = this.Nextbutton.IsEnabled = false;
        }

        #region MyRegion
        /// <summary>
        /// checking if we could switch to this window
        /// </summary>
        /// <param name="state"></param>
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Switching back to the client window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Backbutton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new ClientWindow());
        }
        /// <summary>
        /// Sorting the list of branch depending of the user choice of cashroutlevel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cashroutLevelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // setting the combobox of ListIfBranch's accessibility to true
            this.ComboboxList.IsEnabled = true;
            // trying to select specific branch
            try
            {
                // calling function to sort the branch
                this.ComboboxList.ItemsSource = bl.CheckLevelOfCahrout((BE.CasheRoutLevel)cashroutLevelComboBox.SelectedValue);

                // checking if we have matches
                if (ComboboxList.Items.Count == 0)
                {
                    throw new Exception("there is no branch available with this level of cashrout, please choose another one");
                }
            }
            catch (Exception ex)
            {
                // catching exception
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        /// <summary>
        /// Checking the value of the combobox in order to unlock button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboboxList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboboxList.SelectedValue != null)
                this.Nextbutton.IsEnabled = true;
        }
        /// <summary>
        /// Switching windows and Add a new Order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Nextbutton_Click(object sender, RoutedEventArgs e)
        {
            // trying to add new order depending on user choice
            try
            {
                // function calling
                bl.AddOrder(new Order { choice = (BE.CasheRoutLevel)cashroutLevelComboBox.SelectedValue, NumberOfDeliveryManAvailable = ((Branch)ComboboxList.SelectedValue).NumberOfWorker, BranchMispar = ((Branch)ComboboxList.SelectedValue).BranchNumber, Date = DateTime.Now });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            Switcher.Switch(new MenuOrderWindow(((Branch)ComboboxList.SelectedValue).BranchNumber));
        }
        #endregion
        
    }
}
