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
    /// Logique d'interaction pour UpdateBranch.xaml
    /// </summary>
    public partial class UpdateBranch : UserControl, iSwitchable
    {
        Branch branch;
        IBL bl;
        /// <summary>
        /// Constructor , initialize data
        /// </summary>
        public UpdateBranch()
        {
            InitializeComponent();
            branch = new Branch();
            this.DataContext = branch;
            bl = Factory_BL.GetBL();
            // loading data
            this.ListBranch.ItemsSource = bl.ListOfBranch();
            this.cashroutLevelComboBox.ItemsSource = Enum.GetValues(typeof(BE.CasheRoutLevel));
            // lock all the button
            this.branchNameTextBox.IsEnabled = branchAddressTextBox.IsEnabled = branchNumberTelephoneTextBox.IsEnabled = nameInchargeTextBox.IsEnabled = numberOfWorkerTextBox.IsEnabled = numberDeliveryManAvailabeTextBox.IsEnabled = cashroutLevelComboBox.IsEnabled = false;
        }

        #region Event
        /// <summary>
        /// Get back to main Operation Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Operation("Branch"));
        }
        /// <summary>
        /// Checking the value of the combobox, in order to unlock all the button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBranch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBranch.SelectedValue != null)
            {
                // unlock all button and assign value
                this.branchNameTextBox.IsEnabled = branchAddressTextBox.IsEnabled = branchNumberTelephoneTextBox.IsEnabled = nameInchargeTextBox.IsEnabled = numberOfWorkerTextBox.IsEnabled = numberDeliveryManAvailabeTextBox.IsEnabled = cashroutLevelComboBox.IsEnabled = true;
            }
        }
        /// <summary>
        /// Update branch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // calling function
                bl.UpdateBranch(((Branch)ListBranch.SelectedValue).BranchNumber, branch);
                // refreshing the list
                ListBranch.ItemsSource = bl.ListOfBranch();
                // confirmation
                MessageBox.Show("Branch succefully update");
                // reinitialize data
                branch = new Branch();
                this.DataContext = branch;
            }
            // in case we have an exception we catch it
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
        /// <summary>
        /// Checking the value of the textbox, we sould receive a string not a number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void branchNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if we succefully convert the text of the textbox it means that we only have number which is forbidden
            int tmp;
            if (int.TryParse(branchNameTextBox.Text, out tmp))
            {
                // reinitialize the text 
                branchNameTextBox.Text = "";
            }
        }
        /// <summary>
        /// Checking the value of the textbox, we sould receive a string not a number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nameInchargeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if we succefully convert the text of the textbox it means that we only have number which is forbidden
            int tmp;
            if (int.TryParse(nameInchargeTextBox.Text, out tmp))
            {
                // reinitialize the text 
                nameInchargeTextBox.Text = "";
            }
        }
        /// <summary>
        /// Checking the value of the textbox, we sould receive a number not a single letter is allowed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void branchNumberTelephoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if we succefully convert the text of the textbox it means that we only have number which is forbidden
            int tmp;
            if (!int.TryParse(branchNumberTelephoneTextBox.Text, out tmp))
            {
                // reinitialize the text 
                branchNumberTelephoneTextBox.Text = "";
            }
        }
        /// <summary>
        /// Checking the value of the textbox, we sould receive a number not a single letter is allowed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numberDeliveryManAvailabeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if we have some trouble to convert the text of the textbox it means that we have non number character which is forbidden
            int tmp;
            if (!int.TryParse(numberDeliveryManAvailabeTextBox.Text, out tmp))
            {
                // reinitialize the text 
                numberDeliveryManAvailabeTextBox.Text = "";
            }
        }
        /// <summary>
        /// Checking the value of the textbox, we sould receive a number not a single letter is allowed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numberOfWorkerTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if we have some trouble to convert the text of the textbox it means that we have non number character which is forbidden
            int tmp;
            if (!int.TryParse(numberOfWorkerTextBox.Text, out tmp))
            {
                // reinitialize the text 
                numberOfWorkerTextBox.Text = "";
            }
        }
        /// <summary>
        /// Checking the value of the textbox, we sould receive a string not a number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void branchAddressTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if we succefully convert the text of the textbox it means that we only have number which is forbidden
            int tmp;
            if (int.TryParse(branchAddressTextBox.Text, out tmp))
            {
                // reinitialize the text 
                branchAddressTextBox.Text = "";
            }
        }
        #endregion  
    }
}
