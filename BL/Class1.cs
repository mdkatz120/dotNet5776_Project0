// Simon Moyal 1177707
//David Katz 065970394
using System;
using System.Collections.Generic;
using System.Linq;
using BE;

namespace BL
{
    /// <summary>
    /// interface of Business Layer, getting and asking information to the dal
    /// </summary>
    public interface IBL
    {
        #region Dish's Function
        void AddDish(Dish dish);
        bool DeleteDish(int ID);
        void UpdateDish(int OldID, Dish dish);
        IEnumerable<Dish> ListOfDish(Func<Dish, bool> predicate = null);
        #endregion

        #region Branch's Functions
        void AddBranch(Branch branch);
        bool DeleteBranch(int number);
        void UpdateBranch(int Oldnumber, Branch branch);
        int GetNumberOfDeliveryMan(int numberOrder);
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
        IEnumerable<Ordered_Dish> ListOfOrdered(Func<Ordered_Dish, bool> predicate = null);
        #endregion

        #region Function's IBL
        double TotalPrice(int NumberOrder);
        bool IsExecissivePrice(double price, int MaxPrice);
        IEnumerable<Branch> CheckLevelOfCahrout(CasheRoutLevel Specific);
        IEnumerable<Dish> ByCashRoutonly(CasheRoutLevel Specific);
        IEnumerable<IGrouping<int, double>> GainsByDishes();
        IEnumerable<IGrouping<DateTime, double>> GainsByPeriods();
        IEnumerable<IGrouping<string, double>> GainByLocation();
        List<Order> SearchSpecificOrder(Func<Order, bool> theCondition);
        bool isEligibedForOrder(Client client);
        CasheRoutLevel FindCashRoutLevelByOrderNumber(int ordernumber);
        double FindPriceByID(int ID);
        bool IsExistingID(int ID, CasheRoutLevel choice);
        void AddClient(Client client);
        int FindTheLonguestDish(int ordernumber);
        string NameByID(int id);
        #endregion
    }
}
