// Simon Moyal 1177707
//David Katz 065970394
using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using DAL;


namespace BL
{
    // implementation of BL
    public class IBL_imp : IBL
    {
        public idal obj;
        /// <summary>
        /// Constructor and getting dal
        /// </summary>
        public IBL_imp()
        {
            obj = DAL.FactoryDal.GetDAl();
        }

        #region Branch's Function
        /// <summary>
        /// Add function 
        /// </summary>
        /// <param name="branch"></param>
        public void AddBranch(Branch branch)
        {
            // calling function from the dal
            obj.AddBranch(branch);
        }
        /// <summary>
        /// Delete a branch function
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool DeleteBranch(int number)
        {
            // calling function from the dal
            return obj.DeleteBranch(number);
        }
        /// <summary>
        /// Update info of a branch
        /// </summary>
        /// <param name="branch"></param>
        public void UpdateBranch(int Oldnumber, Branch branch)
        {
            // calling function from the dal
            obj.UpdateBranch(Oldnumber, branch);
        }
        /// <summary>
        /// Getting the amount of Delivery man
        /// </summary>
        /// <param name="numberOrder"></param>
        /// <returns></returns>
        public int GetNumberOfDeliveryMan(int numberOrder)
        {
            // searching for the order
            Order tmp = obj.ListOfOrder(x => x.OrderNumber == numberOrder).FirstOrDefault();
            if (tmp == null)
                throw new Exception("Incorrect numberOrder");

            // searching for the branch
            Branch final = obj.ListOfBranch(b => b.BranchNumber == tmp.BranchMispar).FirstOrDefault();
            if (final == null)
                throw new Exception("this branch doesn't exist anymore");

            // testing if we have some guys free
            if (final.NumberDeliveryManAvailabe - 1 < 0)
                return 0;

            // return the number
            return --final.NumberDeliveryManAvailabe;
        }
        /// <summary>
        /// getting the list, or just special element
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Branch> ListOfBranch(Func<Branch, bool> predicate = null)
        {
            return obj.ListOfBranch(predicate);
        }
        #endregion

        #region Dish's Function
        /// <summary>
        /// Add Dish
        /// </summary>
        /// <param name="dish"></param>
        public void AddDish(Dish dish)
        {
            obj.AddDish(dish);
        }
        public bool DeleteDish(int ID)
        {
            return obj.DeleteDish(ID);
        }
        public void UpdateDish(int OldID, Dish dish)
        {
            obj.UpdateDish(OldID, dish);
        }
        /// <summary>
        /// getting the list, or just special element
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Dish> ListOfDish(Func<Dish, bool> predicate = null)
        {
            return obj.ListOfDish(predicate);
        }
        #endregion

        #region Order's Function
        /// <summary>
        /// Add Order function
        /// </summary>
        /// <param name="order"></param>
        public void AddOrder(Order order)
        {
            obj.AddOrder(order);
        }
        /// <summary>
        /// Delete order
        /// </summary>
        /// <param name="NumberOrder"></param>
        /// <returns></returns>
        public bool DeleteOrder(int NumberOrder)
        {
            return obj.DeleteOrder(NumberOrder);
        }

        /// <summary>
        /// getting the list, or just special element
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Order> ListOfOrder(Func<Order, bool> predicate = null)
        {
            return obj.ListOfOrder(predicate);
        }
        #endregion

        #region Ordered-Dish's Functions 
        /// <summary>
        /// Add Ordered dish 
        /// </summary>
        /// <param name="orderdish"></param>
        public void AddOrdered(Ordered_Dish orderdish)
        {
            obj.AddOrdered(orderdish);
        }
        /// <summary>
        /// delete all ordered dish
        /// </summary>
        /// <param name="OrderNumber"></param>
        public void DeleteAllOrdered(int OrderNumber)
        {
            obj.DeleteAllOrdered(OrderNumber);
        }
        /// <summary>
        /// Delete specific ordered dish
        /// </summary>
        /// <param name="OrderNumber"></param>
        /// <param name="DishID"></param>
        /// <param name="Quantity"></param>
        /// <returns></returns>
        public bool DeleteSpecificOrdered(int OrderNumber, int DishID, int Quantity)
        {
            return obj.DeleteSpecificOrdered(OrderNumber, DishID, Quantity);
        }
        /// <summary>
        /// getting the list, or just special element
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Ordered_Dish> ListOfOrdered(Func<Ordered_Dish, bool> predicate = null)
        {
            return obj.ListOfOrdered(predicate);
        }
        #endregion

        #region IBL Function
        /// <summary>
        /// Total Price function : Reflexion , each time we made a new ordered dish for A unique client , we put the number into ordereddish property
        /// so each time we have the same numberorder, we continue to calcul the final price
        /// </summary>
        /// <param name="NumberOrder"></param>
        /// <returns></returns>
        public double TotalPrice(int NumberOrder)
        {
            double price = 0;
            // checking the lidt
            if (ListOfOrdered().Count() == 0)
                return 0;
            foreach (var item in ListOfOrdered(x => x.OrderNumber == NumberOrder))
            {
                // Price * Quantity
                price += item.Price * item.QuantityOrder; ;
            }
            return price;
        }
        /// <summary>
        /// MaxPrice checking
        /// </summary>
        /// <param name="price"></param>
        /// <param name="MaxPrice"></param>
        /// <returns></returns>
        public bool IsExecissivePrice(double price, int MaxPrice)
        {
            if (price > MaxPrice)
                throw new Exception("Execissive price");

            else
                return false;
        }
        public bool isEligibedForOrder(Client client)
        {
            if (client.age < 18)
                throw new Exception("Your so young , you can't order");
            else
                return true;
        }
        /// <summary>
        /// Getting all the branch with a specific cashrout level
        /// </summary>
        /// <param name="Specific"></param>
        /// <returns></returns>
        public IEnumerable<Branch> CheckLevelOfCahrout(CasheRoutLevel Specific)
        {
            return ListOfBranch(item => item.CashroutLevel == Specific);
        }
        /// <summary>
        /// Getting all the dish with a specific cashrout level + XML
        /// </summary>
        /// <param name="Specific"></param>
        /// <returns></returns>
        public IEnumerable<Dish> ByCashRoutonly(CasheRoutLevel Specific)
        {
            return obj.ListOfDish(item => item.Request == Specific);
        }
        /// <summary>
        /// Grouping by GainbyDishes
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<int, double>> GainsByDishes()
        {
            // group the dish by checking if the ordered list the most ordered dish
            return from item in ListOfDish()
                   from item2 in ListOfOrdered()
                   where item.MenuID == item2.DishID
                   group item2.Price * item2.QuantityOrder by item2.DishID;
        }
        /// <summary>
        /// Grouping by date
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<DateTime, double>> GainsByPeriods()
        {
            // group the most rentability date by checking the date in the ordered list
            return from item in ListOfOrder()
                   from item2 in ListOfOrdered()
                   where item.OrderNumber == item2.OrderNumber
                   group item2.Price * item2.QuantityOrder by item.Date;
        }
        public IEnumerable<IGrouping<string, double>> GainByLocation()
        {
            return from item in obj.ListOfBranch()
                   from item2 in obj.ListOfOrdered()
                   where item.BranchNumber == item2.BranchID
                   group item2.Price * item2.QuantityOrder by item.BranchName;
        }
        /// <summary>
        /// getting all the order matching with a condition
        /// </summary>
        /// <param name="SpecificCondition"></param>
        /// <returns></returns>
        public List<Order> SearchSpecificOrder(Func<Order, bool> SpecificCondition)
        {
            return (from S in ListOfOrder(SpecificCondition) select S).ToList();
        }
        /// <summary>
        /// getting the cashrout level by an ordernumber
        /// </summary>
        /// <param name="ordernumber"></param>
        /// <returns></returns>
        public CasheRoutLevel FindCashRoutLevelByOrderNumber(int ordernumber)
        {
            // searching
            Order tmp = ListOfOrder(x => x.OrderNumber == ordernumber).FirstOrDefault();
            if (tmp == null)
                throw new Exception("Can't find the order");

            // return the choice
            return tmp.choice;
        }
        /// <summary>
        /// Find price of a dish by it's id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public double FindPriceByID(int ID)
        {
            // searching
            Dish tmp = ListOfDish(d => d.MenuID == ID).FirstOrDefault();
            if (tmp == null)
                throw new Exception("can't find dish for this id");
            // return price
            return tmp.PriceOfDish;
        }
        /// <summary>
        /// Checking the ID
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="choice"></param>
        /// <returns></returns>
        public bool IsExistingID(int ID, CasheRoutLevel choice)
        {
            // searching 
            Dish tmp = ListOfDish(d => d.MenuID == ID && d.Request == choice).FirstOrDefault();
            if (tmp == null)
                throw new Exception("can't find dish for this id");
            return true;
        }
        /// <summary>
        /// add client
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(Client client)
        {
            obj.AddClient(client);
        }
        /// <summary>
        /// find longuest dish's preparation time, for indicating estimate time for livraison
        /// </summary>
        /// <param name="ordernumber"></param>
        /// <returns></returns>
        public int FindTheLonguestDish(int ordernumber)
        {
            int time = 0;
            // searching throught the list
            foreach (var item in obj.ListOfOrdered(x => x.OrderNumber == ordernumber))
            {
                // still checking , avoid throw excpetion
                Dish tmp = obj.ListOfDish(d => d.MenuID == item.DishID).FirstOrDefault();
                if (tmp == null)
                    throw new Exception("Incorrect data");
                // swapping
                if (time < tmp.PreparationTime)
                    time = tmp.PreparationTime;
            }
            // return the time
            return time;
        }
        /// <summary>
        /// getting the name of dish by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string NameByID(int id)
        {
            Dish tmp = ListOfDish(d => d.MenuID == id).FirstOrDefault();
            if (tmp == null)
                throw new Exception("the id doesn't exist");
            return tmp.Name;
        }
        #endregion
    }
}
