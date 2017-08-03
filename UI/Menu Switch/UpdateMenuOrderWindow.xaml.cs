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
    /// Logique d'interaction pour UpdateMenuOrderWindow.xaml
    /// </summary>
    public partial class UpdateMenuOrderWindow : UserControl, iSwitchable
    {
        Ordered_Dish order;
        IBL bl;
        /// <summary>
        /// Constructor , initialize data , locking button 
        /// </summary>
        public UpdateMenuOrderWindow()
        {
            InitializeComponent();
            order = new Ordered_Dish();
            this.DataContext = order;
            bl = Factory_BL.GetBL();
            this.OrderListCombobox.ItemsSource = bl.ListOfOrder();
            this.DateTimetrueLabel.Content = DateTime.Now;
            this.SearchButton.IsEnabled = this.NextButton.IsEnabled = false;
            this.menuIDTextBox.IsEnabled = this.PriceTextBox.IsEnabled = this.Nametextbox.IsEnabled  = ListOfDishCombobox.IsEnabled = false;
            this.quantityTextBox.IsEnabled = this.SearchButton.IsEnabled = false;
        }

        #region Event
        /// <summary>
        ///checking if we could switch to this window
        /// </summary>
        /// <param name="state"></param>
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// In order to search by name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Nametextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if we succefully convert the text of the textbox it means that we only have number which is forbidden
            int tmp;
            if (int.TryParse(Nametextbox.Text, out tmp))
            {
                Nametextbox.Text = "";
                this.SearchButton.IsEnabled = false;
            }
            else
                // unlock button
                this.SearchButton.IsEnabled = true;
        }
        /// <summary>
        /// In order to search by price
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PriceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int tmp;
            if (!Int32.TryParse(PriceTextBox.Text, out tmp))
            {
                PriceTextBox.Text = "";
            }
            else
                // unlock button
                this.SearchButton.IsEnabled = true;
        }
        /// <summary>
        /// MenuID textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuIDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if we have some trouble to convert the text of the textbox it means that we have non number character which is forbidden
            int tmp;
            if (!Int32.TryParse(menuIDTextBox.Text, out tmp) && menuIDTextBox.Text != "")
            {
                menuIDTextBox.Text = "0";
            }
        }
        /// <summary>
        /// Quantity textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quantityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if we have some trouble to convert the text of the textbox it means that we have non number character which is forbidden
            int tmp;
            if (!Int32.TryParse(quantityTextBox.Text, out tmp) && menuIDTextBox.Text != "")
            {
                quantityTextBox.Text = "0";
            }
        }
        /// <summary>
        /// Add an ordereddish
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddOrderedButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /* Message : i know this isn't useful if we made data binding but i have a problem with datacontext
                    he just work a time after that all not working so i was forced to use this ...
                 */
                order.DishID = int.Parse(menuIDTextBox.Text);
                Order test = (Order)OrderListCombobox.SelectedValue;
                CasheRoutLevel test2 = bl.FindCashRoutLevelByOrderNumber(test.OrderNumber);
                order.OrderNumber = test.OrderNumber;
                order.QuantityOrder = int.Parse(quantityTextBox.Text);

                // checking is the id exist
                if (bl.IsExistingID(order.DishID, bl.FindCashRoutLevelByOrderNumber(order.OrderNumber)))
                {
                    // setting price
                    order.Price = bl.FindPriceByID(order.DishID);
                    order.name = bl.NameByID(order.DishID);
                    bl.IsExecissivePrice(bl.TotalPrice(order.OrderNumber) + order.Price * order.QuantityOrder, 700);
                    // adding the ordered dish and trying to reduce the quantity ordered
                    bl.AddOrdered(order);
                    // refreshing the price
                    TotalPriceLabel.Content = bl.TotalPrice(order.OrderNumber).ToString();
                    // refreshing the ListOfDish with new quantity
                    ListOfDishCombobox.ItemsSource = bl.ListOfDish(x => x.Request == test2);
                    // reinitialise data
                    order = new Ordered_Dish();
                    this.NextButton.IsEnabled = true;
                    this.menuIDTextBox.Text = this.quantityTextBox.Text = "0";
                    this.OrderedCombobox.ItemsSource = bl.ListOfOrdered(x => x.OrderNumber == ((Order)OrderListCombobox.SelectedValue).OrderNumber);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Delete an ordered dish
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /* Message : i know this isn't the better way if we made data binding but i have a problem with datacontext
                    he just work a time after that all not working so i was forced to use this ...
                 */
                Order test = (Order)OrderListCombobox.SelectedValue;
                CasheRoutLevel test2 = bl.FindCashRoutLevelByOrderNumber(test.OrderNumber);
                order.OrderNumber = test.OrderNumber;
                order.QuantityOrder = int.Parse(quantityTextBox.Text);
                order.DishID = int.Parse(menuIDTextBox.Text);

                // delete the dish
                bl.DeleteSpecificOrdered(order.OrderNumber, order.DishID, order.QuantityOrder);
                // refreshing the list of dish
                ListOfDishCombobox.ItemsSource = bl.ListOfDish(x => x.Request == test2);
                // refreshing the price
                TotalPriceLabel.Content = bl.TotalPrice(order.OrderNumber);
                // reinitialize data
                order = new Ordered_Dish();
                this.NextButton.IsEnabled = true;
                this.menuIDTextBox.Text = this.quantityTextBox.Text = "0";
                // refreshing the orderedllist
                this.OrderedCombobox.ItemsSource = bl.ListOfOrdered(x => x.OrderNumber == ((Order)OrderListCombobox.SelectedValue).OrderNumber);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Checking the value of the combobox in order to unlock button and loading the combobox with the selected value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderListCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrderListCombobox.SelectedValue != null)
            {
                this.menuIDTextBox.IsEnabled = this.PriceTextBox.IsEnabled = this.Nametextbox.IsEnabled = menuIDTextBox.IsEnabled = quantityTextBox.IsEnabled = ListOfDishCombobox.IsEnabled = true;
                // loading the list of dish
                this.ListOfDishCombobox.ItemsSource = bl.ListOfDish(item => item.Request == bl.FindCashRoutLevelByOrderNumber(((Order)OrderListCombobox.SelectedValue).OrderNumber));
                // loading the ordered dish
                this.OrderedCombobox.ItemsSource = bl.ListOfOrdered(x => x.OrderNumber == ((Order)OrderListCombobox.SelectedValue).OrderNumber);
                // displaying the price
                TotalPriceLabel.Content = bl.TotalPrice(((Order)OrderListCombobox.SelectedValue).OrderNumber);
            }
        }
        /// <summary>
        /// Search button , find specific dish with some filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Order test = (Order)OrderListCombobox.SelectedValue;
            CasheRoutLevel test2 = bl.FindCashRoutLevelByOrderNumber(test.OrderNumber);
            order.OrderNumber = test.OrderNumber;
            // search by name
            if (PriceTextBox.Text == "" && Nametextbox.Text != "")
            {
                // if we don't have matching , reinitialise the dish list
                if (bl.ListOfDish(x => x.Request == test2 && x.Name.Contains(Nametextbox.Text)).Count() == 0)
                {
                    MessageBox.Show("No matching found");
                    Nametextbox.Text = "";
                    this.ListOfDishCombobox.ItemsSource = bl.ByCashRoutonly(test2);
                }
                else
                    this.ListOfDishCombobox.ItemsSource = bl.ListOfDish(x => x.Name.Contains(Nametextbox.Text) && x.Request == test2);
            }
            // search by price
            else if (PriceTextBox.Text != "" && Nametextbox.Text == "")
            {
                // if we don't have matching , reinitialise the dish list
                if (bl.ListOfDish(x => x.PriceOfDish <= int.Parse(PriceTextBox.Text) && x.Request == test2).Count() == 0)
                {
                    MessageBox.Show("No matching found");
                    PriceTextBox.Text = "";
                    this.ListOfDishCombobox.ItemsSource = bl.ByCashRoutonly(test2);
                }
                else
                    // searching
                    this.ListOfDishCombobox.ItemsSource = bl.ListOfDish(x => x.PriceOfDish <= int.Parse(PriceTextBox.Text) && x.Request == test2);

            }
            // search by price and name
            else if (PriceTextBox.Text != "" && Nametextbox.Text != "")
            {
                // if we don't have matching , reinitialise the dish list
                if (bl.ListOfDish(x => x.PriceOfDish <= int.Parse(PriceTextBox.Text) && x.Name.Contains(Nametextbox.Text) && x.Request == test2).Count() == 0)
                {
                    MessageBox.Show("No matching found");
                    PriceTextBox.Text = "";
                    Nametextbox.Text = "";
                    this.ListOfDishCombobox.ItemsSource = bl.ByCashRoutonly(test2);
                }
                else
                    // searching  
                    this.ListOfDishCombobox.ItemsSource = bl.ListOfDish(x => x.PriceOfDish <= int.Parse(PriceTextBox.Text) && x.Name.Contains(Nametextbox.Text) && x.Request == test2);
            }
            else
                // if we don't have nothing
                this.ListOfDishCombobox.ItemsSource = bl.ByCashRoutonly(test2);
        }
        /// <summary>
        /// Confirmation of the order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Congrats, we have succefully update your command\n " +
                "Your number order is:  " + ((Order)OrderListCombobox.SelectedValue).OrderNumber +
                "\nThe Price for your order is:  " +  TotalPriceLabel.Content /*bl.TotalPrice(bl.ListOfOrder().Last().OrderNumber)*/ +
                "€\nThe delay time is around: " + (bl.FindTheLonguestDish(bl.ListOfOrder().Last().OrderNumber) + 15) + "min" +
                "\nThanks for the order and BON APPETIT", "Summary", MessageBoxButton.OK, MessageBoxImage.Information);
            Switcher.Switch(new MainWindow());
        }
        #endregion
    }
}
