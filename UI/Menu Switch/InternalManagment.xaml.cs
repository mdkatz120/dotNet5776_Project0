// Simon Moyal 1177707
// David Katz 065970394
using System;
using System.Windows;
using System.Windows.Controls;

namespace UI.Menu_Switch
{
    /// <summary>
    /// Logique d'interaction pour InternalManagment.xaml
    /// </summary>
    public partial class InternalManagment : UserControl, iSwitchable
    {
        /// <summary>
        /// all those action are specially for the person incharge of the restaurant
        /// </summary>
        public InternalManagment()
        {
            InitializeComponent();
        }

        #region Myregion
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Switching back to the main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainWindow());
        }
        /// <summary>
        /// Switching to the operation window with the information "Dish"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DishButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Operation("Dish"));
        }
        /// <summary>
        /// Switching to the operation window with the information "Branch"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BranchButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Operation("Branch"));
        }
        /// <summary>
        /// Switching to the finacial window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GainsButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Financial());
        }
        #endregion
    }
}
