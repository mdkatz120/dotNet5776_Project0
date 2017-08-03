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
    /// Logique d'interaction pour MenuOrderWindow.xaml
    /// </summary>
    public partial class MenuOrderWindow : UserControl, iSwitchable
    {
        Ordered_Dish order;
        IBL bl;
        int Branch;
        /// <summary>
        /// Constructor , initialize data, locking button
        /// </summary>
        /// <param name="branch"></param>
        public MenuOrderWindow(int branch)
        {
            InitializeComponent();
            this.Branch = branch;
            order = new Ordered_Dish();
            this.DataContext = order;
            bl = Factory_BL.GetBL();
            this.NumberOrderLabel.Content = bl.ListOfOrder().Last().OrderNumber;
            this.DateTimetrueLabel.Content = DateTime.Now; 
            this.ListOfDishCombobox.ItemsSource = bl.ByCashRoutonly(bl.ListOfOrder().Last().choice);
            this.SearchButton.IsEnabled = this.NextButton.IsEnabled = false;
            this.menuIDTextBox.Text = this.PriceTextBox.Text = this.quantityTextBox.Text = Nametextbox.Text = "";
        }

        #region Event
        /// <summary>
        /// checking if we could switch to this window
        /// </summary>
        /// <param name="state"></param>
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Add new ordered dish
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddOrderedButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                order.OrderNumber = bl.ListOfOrder().Last().OrderNumber;
                // checking is the id exist
                if (bl.IsExistingID(order.DishID, bl.ListOfOrder().Last().choice))
                {
                    // setting price
                    order.Price = bl.FindPriceByID(order.DishID);
                    bl.IsExecissivePrice((bl.TotalPrice(order.OrderNumber) + order.QuantityOrder * order.Price), 1500);
                    // adding the ordered dish and trying to reduce the quantity ordered
                    order.name = bl.NameByID(order.DishID);
                    order.BranchID = Branch;
                    bl.AddOrdered(order);
                    // refreshing the ListOfDish with new quantity
                    ListOfDishCombobox.ItemsSource = bl.ListOfDish(x => x.Request == bl.ListOfOrder().Last().choice);
                    // refreshing the price
                    TotalPriceLabel.Content = bl.TotalPrice(order.OrderNumber);
                    // reinitialise data
                    order = new Ordered_Dish();
                    this.DataContext = order;
                    OrderedCombobox.ItemsSource = bl.ListOfOrdered(x => x.OrderNumber == bl.ListOfOrder().Last().OrderNumber);
                    // we have at least one element ordered so we can continue to newt step , taking information of the client
                    this.NextButton.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                this.SearchButton.IsEnabled = true;
        }
        /// <summary>
        /// In order to search by price
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PriceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if we have some trouble to convert the text of the textbox it means that we have non number character which is forbidden
            int tmp;
            if (!int.TryParse(PriceTextBox.Text, out tmp))
            {
                PriceTextBox.Text = "";
                this.SearchButton.IsEnabled = false;
            }
            else
                this.SearchButton.IsEnabled = true;
        }
        /// <summary>
        /// Search button , find specific dish with some filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click_1(object sender, RoutedEventArgs e)
        {
            // search by name
            if (PriceTextBox.Text == "" && Nametextbox.Text != "")
            {
                // if we don't have matching , reinitialise the dish list
                if (bl.ListOfDish(x => x.Request == bl.ListOfOrder().Last().choice && x.Name.Contains(Nametextbox.Text)).Count() == 0)
                {
                    MessageBox.Show("No matching found");
                    Nametextbox.Text = "";
                    this.ListOfDishCombobox.ItemsSource = bl.ByCashRoutonly(bl.ListOfOrder().Last().choice);
                }
                else
                    // searching
                    this.ListOfDishCombobox.ItemsSource = bl.ListOfDish(x => x.Name.Contains(Nametextbox.Text) && x.Request == bl.ListOfOrder().Last().choice);
            }
            // search by price
            else if (PriceTextBox.Text != "" && Nametextbox.Text == "")
            {
                // if we don't have matching , reinitialise the dish list
                if (bl.ListOfDish(x => x.Request == bl.ListOfOrder().Last().choice && x.PriceOfDish <= int.Parse(PriceTextBox.Text)).Count() == 0)
                {
                    MessageBox.Show("No matching found");
                    PriceTextBox.Text = "";
                    this.ListOfDishCombobox.ItemsSource = bl.ByCashRoutonly(bl.ListOfOrder().Last().choice);
                }
                else
                    // searching
                    this.ListOfDishCombobox.ItemsSource = bl.ListOfDish(x => x.PriceOfDish <= int.Parse(PriceTextBox.Text) && x.Request == bl.ListOfOrder().Last().choice);

            }
            // search by price and name
            else if (PriceTextBox.Text != "" && Nametextbox.Text != "")
            {
                // if we don't have matching , reinitialise the dish list
                if (bl.ListOfDish(x => x.Request == bl.ListOfOrder().Last().choice && x.PriceOfDish <= int.Parse(PriceTextBox.Text) && x.Name.Contains(Nametextbox.Text)).Count() == 0)
                {
                    MessageBox.Show("No matching found");
                    PriceTextBox.Text = "";
                    Nametextbox.Text = "";
                    this.ListOfDishCombobox.ItemsSource = bl.ByCashRoutonly(bl.ListOfOrder().Last().choice);
                }
                else
                    // searching
                    this.ListOfDishCombobox.ItemsSource = bl.ListOfDish(x => x.PriceOfDish <= int.Parse(PriceTextBox.Text) && x.Name.Contains(Nametextbox.Text) && x.Request == bl.ListOfOrder().Last().choice);
            }
            else
                this.ListOfDishCombobox.ItemsSource = bl.ByCashRoutonly(bl.ListOfOrder().Last().choice);
        }
        /// <summary>
        /// Verification of the input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuIDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if we have some trouble to convert the text of the textbox it means that we have non number character which is forbidden
            int tmp;
            if (!int.TryParse(menuIDTextBox.Text, out tmp) && menuIDTextBox.Text != "")
            {
                menuIDTextBox.Text = "0";
            }
        }
        /// <summary>
        /// Verification of the input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quantityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if we have some trouble to convert the text of the textbox it means that we have non number character which is forbidden
            int tmp;
            if (!Int32.TryParse(quantityTextBox.Text, out tmp) && quantityTextBox.Text != "")
            {
                quantityTextBox.Text = "0";
            }
        }
        /// <summary>
        /// Back button delete the order and/or all the ordered dish
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                order.OrderNumber = bl.ListOfOrder().Last().OrderNumber;
                // if we haven't do a single ordered dish so we will just delete the order
                if (bl.ListOfOrdered().Count() == 0)
                {
                    bl.DeleteOrder(order.OrderNumber);
                }
                // or we will completly remove the order and the ordereddish
                else
                {
                    bl.DeleteAllOrdered(order.OrderNumber);
                }
                Switcher.Switch(new NewOrderWindow());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Confirmation of the command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            // if we have some guys available
            if (bl.GetNumberOfDeliveryMan(bl.ListOfOrder().Last().OrderNumber) > 0)
            {
                MessageBox.Show("Congrats, we have succefully transfer your command\n " +
                "Your OrderNumber is:  " + bl.ListOfOrder().Last().OrderNumber +
                "\nThe Price for your order is:  " + bl.TotalPrice(bl.ListOfOrder().Last().OrderNumber) +
                "€\nThe delay time is around:   " + (bl.FindTheLonguestDish(bl.ListOfOrder().Last().OrderNumber) + 15) + " min" +
                "\nThanks for the order and BON APPETIT", "Summary", MessageBoxButton.OK, MessageBoxImage.Information);
                Switcher.Switch(new MainWindow());
            }
            else
            // we don't have guys so the time will be more longuer
            {
                MessageBox.Show("Congrats, we have succefully transfer your command\n" +
                "Your OrderNumber is:  " + bl.ListOfOrder().Last().OrderNumber +
                "\nThe Price for your order is:  " + bl.TotalPrice(bl.ListOfOrder().Last().OrderNumber) +
                "€\nWe haven't deliver for the moment so the delay time is around: " + (bl.FindTheLonguestDish(bl.ListOfOrder().Last().OrderNumber) + 25) + " min" +
                "\nThanks for the order and BON APPETIT", "Summary", MessageBoxButton.OK, MessageBoxImage.Information);
                Switcher.Switch(new MainWindow());
            }
        }
        #endregion
    }
}
