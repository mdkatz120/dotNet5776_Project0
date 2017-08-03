// Simon Moyal 1177707
//David Katz 065970394
using System;

namespace BE
{
    /// <summary>
    /// Class Ordered_Dish
    /// </summary>
    public class Ordered_Dish
    {
        /// <summary>
        /// Make liaison with the original order
        /// </summary>
        public int OrderNumber { set; get; }
        /// <summary>
        /// Represent the amount of command
        /// </summary>
        public int QuantityOrder { set; get; }
        /// <summary>
        /// make the liaison with the dish class , in order to getting information
        /// </summary>
        public int DishID { set; get; }
        /// <summary>
        /// make liaison with the branch, in order to know where we have done the order
        /// </summary>
        public int BranchID { get; set; }
        /// <summary>
        /// name of the dish ordered
        /// </summary>
        public string name { set; get; }
        /// <summary>
        /// Prices of the dish, without the quantity
        /// </summary>
        private double Prices;
        public double Price
        {
            set
            {
                if (value <= 0)
                    throw new Exception("Invalid input");
                else
                    Prices = value;
            }
            get
            {
                return Prices;
            }
        }
        /// <summary>
        /// Override ToString(
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Name: {0}, Quantity: {1}, Price: {2}, ID: {3}", name, QuantityOrder, Prices, DishID);
        }
    }
}

