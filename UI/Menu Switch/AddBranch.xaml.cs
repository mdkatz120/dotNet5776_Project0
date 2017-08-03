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
    /// Logique d'interaction pour AddBranch.xaml
    /// </summary>
    public partial class AddBranch : UserControl, iSwitchable
    {
        Branch branch;
        IBL bl;
        /// <summary>
        /// Constructor , initialize the window , data binding, and combobox
        /// </summary>
        public AddBranch()
        {
            InitializeComponent();
            branch = new Branch();
            this.DataContext = branch;
            bl = Factory_BL.GetBL();
            this.cashroutLevelComboBox.ItemsSource = Enum.GetValues(typeof(CasheRoutLevel));
        }

        #region Event
        /// <summary>
        /// Switching back to the previous window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Operation("Branch"));
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
        /// Add a branch button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // avoid stoping the program for an exception
            try
            {
                // getting the choice of the combobox box (with casting)
                branch.CashroutLevel = (CasheRoutLevel)(cashroutLevelComboBox.SelectedValue);
                // calling function off the bl
                bl.AddBranch(branch);
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
        #endregion
    }
}
