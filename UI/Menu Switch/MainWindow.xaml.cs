// Simon Moyal 1177707
// David Katz 065970394
using System;
using System.Windows;
using System.Windows.Controls;

namespace UI.Menu_Switch
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UserControl, iSwitchable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
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
        /// Switching to the order window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Menu_Switch.MainOrder());
        }
        /// <summary>
        /// Switching to the managment window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManagmentLabel_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new InternalManagment());
        }
        #endregion
    }
}
