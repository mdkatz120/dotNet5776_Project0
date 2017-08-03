// Simon Moyal 1177707
// David Katz 065970394
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace UI.Menu_Switch
{
    /// <summary>
    /// Logique d'interaction pour Financial.xaml
    /// </summary>
    public partial class Financial : UserControl, iSwitchable
    {
        BL.IBL bl;
        List<object> ByDish;
        List<object> ByPeriod;
        List<object> ByLocation;
        /// <summary>
        /// Constructor and calling function in order to have the combobox pre-loaded
        /// </summary>
        public Financial()
        {
            InitializeComponent();
            bl = BL.Factory_BL.GetBL();
            ByDish = new List<object>();
            ByPeriod = new List<object>();
            ByLocation = new List<object>();
            GainByDishs();
            GainByDate();
            GainBySniff();
        }
        /// <summary>
        /// Gain by dish
        /// </summary>
        private void GainByDishs()
        {
            // checking if we have something to show
            if (bl.GainsByDishes().Count() == 0)
            {
                GainByDish.IsEnabled = false;
            }
            else
            {
                // implementing the ienumerable return by the function
                foreach (IGrouping<int, double> item in bl.GainsByDishes())
                {
                    ByDish.Add("Name: " + bl.NameByID(item.Key) + ", ID: " + item.Key + ", Gains:  " + item.Sum());
                }
                // refreshing data
                GainByDish.ItemsSource = ByDish;
            }
        }
        /// <summary>
        /// Gain by period
        /// </summary>
        private void GainByDate()
        {
            // checking if we have something to show
            if (bl.GainsByPeriods().ToList().Count() == 0)
            {
                GainDate.IsEnabled = false;
            }
            else
            {
                // implementing the ienumerable return by the function
                foreach (IGrouping<DateTime, double> item in bl.GainsByPeriods())
                {
                    ByPeriod.Add("Period:  " + item.Key + " Gains: " + item.Sum());
                }
                // refreshing data
                GainDate.ItemsSource = ByPeriod;
            }
        }
        /// <summary>
        /// Gain by branchs
        /// </summary>
        private void GainBySniff()
        {
            // checking if we have something to show
            if (bl.GainByLocation().ToList().Count() == 0)
            {
                GainLocation.IsEnabled = false;
            }
            else
            {
                // implementing the ienumerable return by the function
                foreach (IGrouping<string, double> item in bl.GainByLocation())
                {
                    ByLocation.Add("Location: " + item.Key + " Gains: " + item.Sum());
                }
                // refreshing data
                GainLocation.ItemsSource = ByLocation;
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
        /// Switching back to the Intermanagment Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new InternalManagment());
        }
    }
}
