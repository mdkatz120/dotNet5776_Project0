// Simon Moyal 1177707
// David Katz 065970394
using System.Collections.Generic;
using BE;

namespace DS
{
    /// <summary>
    /// DataSource class , contain all information about branch, dish, order, client ...
    /// </summary>
    public class DataSource
    {
        /// <summary>
        /// List containing the dish
        /// </summary>
        private static List<Dish> DishList;
        public List<Dish> ListDish
        {
            get
            {
                return DishList;
            }
            set
            {
                DishList = value;
            }
        }
        /// <summary>
        /// List containing the branch
        /// </summary>
        private static List<Branch> BranchList;
        public List<Branch> ListBranch
        {
            set
            {
                BranchList = value;
            }
            get
            {
                return BranchList;
            }
        }
        /// <summary>
        /// List containing the order
        /// </summary>
        private static List<Order> OrderList;
        public List<Order> ListOrder
        {
            get
            {
                return OrderList;
            }
            set
            {
                OrderList = value;
            }
        }
        /// <summary>
        /// List containing the ordered-Dish
        /// </summary>
        private static List<Ordered_Dish> OrderedDishList;
        public List<Ordered_Dish> ListOrderedDish
        {
            set
            {
                OrderedDishList = value;
            }
            get
            {
                return OrderedDishList;
            }
        }
        /// <summary>
        /// List containing the client
        /// </summary>
        private static List<Client> ClientList;
        public List<Client> ListClient
        {
            set
            {
                ClientList = value;
            }
            get
            {
                return ClientList;
            }
        }
        /// <summary>
        /// Constructor , initialize all the list
        /// </summary>
        public DataSource()
        {
            DishList = new List<Dish>();
            BranchList = new List<Branch>();
            OrderList = new List<Order>();
            OrderedDishList = new List<Ordered_Dish>();
            ClientList = new List<Client>();
        }
    }
}

