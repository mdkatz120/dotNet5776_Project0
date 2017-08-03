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
    /// Logique d'interaction pour ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : UserControl, iSwitchable
    {
        Client client;
        IBL bl;
        /// <summary>
        /// Constructor , initialize data
        /// </summary>
        public ClientWindow()
        {
            InitializeComponent();
            client = new Client();
            this.DataContext = client;
            bl = Factory_BL.GetBL();
        }
        #region Event
        /// <summary>
        /// Add client and switching to an another window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // first checking if the client is eligibe to perfom order
                if (bl.isEligibedForOrder(client))
                {
                    // calling function
                    bl.AddClient(client);
                    // reinitialize data
                    client = new Client();
                    this.DataContext = client;
                    // switching window
                    Switcher.Switch(new NewOrderWindow());
                }
            }
            // catch excpetion
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Back button , switching window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainOrder());
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
        ///Checking the value of the textbox, we sould receive a string not a number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clientNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if we succefully convert the text of the textbox it means that we only have number which is forbidden
            int tmp;
            if (int.TryParse(clientNameTextBox.Text, out tmp))
            {
                clientNameTextBox.Text = "";
            }
        }
        /// <summary>
        /// Checking the value of the textbox, we sould receive a number not a single letter is allowed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ageTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if we have some trouble to convert the text of the textbox it means that we have non number character which is forbidden
            int tmp;
            if (!int.TryParse(ageTextBox.Text, out tmp))
            {
                ageTextBox.Text = "0";
            }
        }
        /// <summary>
        /// Checking the value of the textbox, we sould receive a number not a single letter is allowed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numberTelephoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if we have some trouble to convert the text of the textbox it means that we have non number character which is forbidden
            int tmp;
            if (!int.TryParse(numberTelephoneTextBox.Text, out tmp))
            {
                numberTelephoneTextBox.Text = "0";
            }
        }
        /// <summary>
        /// Checking the value of the textbox, we sould receive a number not a single letter is allowed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void creditCardNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if we have some trouble to convert the text of the textbox it means that we have non number character which is forbidden
            int tmp;
            if (!int.TryParse(creditCardNumberTextBox.Text, out tmp))
            {
                creditCardNumberTextBox.Text = "0";
            }
        }
        #endregion
    }
}
