using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    /// <summary>
    /// Interface idal with all declaration function
    /// </summary>
    public interface idal
    {

        #region Dish's Function
        void AddDish(Dish dish);
        bool DeleteDish(int ID);
        void UpdateDish(int OldID, Dish dish);
        IEnumerable<Dish> ListOfDish(Func<Dish, bool> predicate = null);
        #endregion

        #region Branch's Functions
        void AddBranch(Branch branch);
        bool DeleteBranch(int Number);
        void UpdateBranch(int oldnumber, Branch branch);
        IEnumerable<Branch> ListOfBranch(Func<Branch, bool> predicate = null);
        #endregion

        #region Order's Function's
        void AddOrder(Order order);
        bool DeleteOrder(int NumberOrder);
        IEnumerable<Order> ListOfOrder(Func<Order, bool> predicate = null);

        #endregion

        #region Ordered_dish's Functions
        void AddOrdered(Ordered_Dish orderdish);
        void DeleteAllOrdered(int OrderNumber);
        bool DeleteSpecificOrdered(int OrderNumber, int DishID, int Quantity);
        void IncreaseQuantityAvailable(Ordered_Dish dish);
        IEnumerable<Ordered_Dish> ListOfOrdered(Func<Ordered_Dish, bool> predicate = null);
        #endregion

        #region Client's Function's
        Client SearchTheClient(string address);
        void AddClient(Client client);
        IEnumerable<Client> ListOfClient(Func<Client, bool> predicate = null);
        #endregion

    }
}
