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
    /// Logique d'interaction pour DeleteBranch.xaml
    /// </summary>
    public partial class DeleteBranch : UserControl, iSwitchable
    {
        Branch branch;
        IBL bl;
        /// <summary>
        /// Constructor , initialize data
        /// </summary>
        public DeleteBranch()
        {
            InitializeComponent();
            branch = new Branch();
            this.DataContext = branch;
            bl = Factory_BL.GetBL();
            ListCombobox.ItemsSource = bl.ListOfBranch();
            this.CancelButton.IsEnabled = false;

        }

        #region MyRegion
        /// <summary>
        /// checking if we could switch to this window (if the class implement our interface)
        /// </summary>
        /// <param name="state"></param>
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Switching back to the previous window with a string branch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Operation("Branch"));
        }
        /// <summary>
        /// checking if the selection isn't null, in order to unlock button and getting the information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListCombobox.SelectedValue != null)
            {
                branch = (Branch)ListCombobox.SelectedValue;
                this.CancelButton.IsEnabled = true;
            }
        }
        /// <summary>
        /// delete function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // asking confirmation to the user
            if (MessageBox.Show("Are you sur to delete this branch ? this action is irreversible", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    // calling function
                    bl.DeleteBranch(branch.BranchNumber);
                    // confirmation
                    MessageBox.Show("The branch has been succefully delete");
                    // refreshing list
                    ListCombobox.ItemsSource = bl.ListOfBranch(x => x.BranchNumber != branch.BranchNumber);
                    // reset data
                    branch = new Branch();
                    this.DataContext = branch;
                    // re-unlock the button
                    this.CancelButton.IsEnabled = false;
                }
                // catching error
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        #endregion

    }
}
