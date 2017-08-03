// Simon Moyal 1177707
// David Katz 065970394
using System;
using System.Windows;
using System.Windows.Controls;

namespace UI.Menu_Switch
{
    /// <summary>
    /// Logique d'interaction pour Operation.xaml
    /// </summary>
    public partial class Operation : UserControl, iSwitchable
    {
        string st;
        /// <summary>
        /// Constructor , we receive a string from precedent window
        /// we did not want to have to do both with the same menu window, depending on the received word we redirect the user directly
        /// </summary>
        /// <param name="wanted"></param>
        public Operation(string wanted)
        {
            this.st = wanted;
            InitializeComponent();
        }

        #region
        /// <summary>
        /// checking if we could switch to this window
        /// </summary>
        /// <param name="state"></param>
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// redirect the user to the add window of his previous choice (branch or dish)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (st == "Dish")
                Switcher.Switch(new AddDish());

            else
                Switcher.Switch(new AddBranch());
        }
        /// <summary>
        /// redirect the user to the update window of his choice (branch or dish)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void update_Click(object sender, RoutedEventArgs e)
        {
            if (st == "Dish")
            {
                Switcher.Switch(new UpdateDish());
            }
            else
                Switcher.Switch(new UpdateBranch());
        }
        /// <summary>
        /// redirect the user to the delete window of his previous choice (branch or dish)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (st == "Branch")
                Switcher.Switch(new DeleteBranch());
            else
                Switcher.Switch(new DeleteDish());
        }
        /// <summary>
        /// Switching back to the internalmanagment window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new InternalManagment());
        }
        #endregion
    }
}
