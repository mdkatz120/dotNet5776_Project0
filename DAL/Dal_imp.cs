using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    /// <summary>
    /// implementation of the interface idal
    /// </summary>
    public class Dal_imp : idal
    {
        // getting data
        static DataSource data;
        // if we have a dish, branchnumber, or clientid = 0 , the dal have to find out a new one id
        public static int CurrentDishID = 1, CurrentBranchNumber = 1, CurrentClientID = 1;
        public static int CurrentOrderID = 10000000;

        public Dal_imp()
        {
            if (data == null)
                data = new DataSource();
        }
        
        #region Order's Function
        /// <summary>
        /// Add function, assignin new OrderID and adding to the list
        /// </summary>
        /// <param name="order"></param>
        public void AddOrder(Order order)
        {
            order.OrderNumber = CurrentOrderID++;
            data.ListOrder.Add(order);
        }
        /// <summary>
        /// Remove Order fom the list
        /// </summary>
        /// <param name="NumberOrder"></param>
        /// <returns></returns>
        public bool DeleteOrder(int NumberOrder)
        {
            // checking if the numberorder is in our list
            Order result = data.ListOrder.FirstOrDefault(d => d.OrderNumber == NumberOrder);
            // no matching
            if (result == null)
                throw new Exception("The order hasn't been found");
            //remove
            else
                return data.ListOrder.Remove(result);
        }
        /// <summary>
        /// Update Order
        /// </summary>
        /// <param name="order"></param>
        public void UpdateOrder(Order order)
        {
            // find the index in the list
            int index = data.ListOrder.FindIndex(o => o.OrderNumber == order.OrderNumber);
            // no matching
            if (index == -1)
                throw new Exception("the order hasn't been found");

            // update data
            data.ListOrder[index] = order; 
        }
        /// <summary>
        /// getting the list
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Order> ListOfOrder(Func<Order, bool> predicate = null)
        {
            if (predicate == null)
                return data.ListOrder.AsEnumerable();

            return data.ListOrder.Where(predicate);
        }
        #endregion

        #region Branch's Function
        /// <summary>
        /// Adding new branch
        /// </summary>
        /// <param name="branch"></param>
        public void AddBranch(Branch branch)
        {
            // first verification if we already have this branch number or number telephone (both can't have double)
            if (!data.ListBranch.Where(b => b.BranchNumberTelephone == branch.BranchNumberTelephone).Any())
            {
                branch.BranchNumber = CurrentBranchNumber++;
                data.ListBranch.Add(branch);
            }
            else
              throw new Exception("a branch with the same ID/Number telephone already exist");
        }
        /// <summary>
        /// Finding new ID
        /// </summary>
        /// <returns></returns>
        public int FindNewID()
        {
            while (true)
            {
                int counter = 0;
                // incrementing and testing in order to get an free id
                foreach (Branch item in data.ListBranch)
                {
                    // checking
                    if (CurrentBranchNumber == item.BranchNumber)
                    {
                        CurrentBranchNumber++;
                        counter++;
                        break;
                    }
                }
                // the ID is free of use now
                if (counter == 0)
                    break;
            }
            return CurrentBranchNumber;
        }
        /// <summary>
        /// Delete branch
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool DeleteBranch(int number)
        {
            // searching the branch
            Branch tmp = data.ListBranch.FirstOrDefault(b => b.BranchNumber == number);
            if (tmp == null)
                throw new Exception("The Branch hasn't been found");

            // delete it
            return data.ListBranch.Remove(tmp);
        }
        /// <summary>
        /// Update information on the branch
        /// </summary>
        /// <param name="branch"></param>
        public void UpdateBranch(int Oldnumber, Branch branch)
        {
            Branch tmp = data.ListBranch.Where(x => x.BranchNumber == Oldnumber).FirstOrDefault();
            if (tmp == null)
                throw new Exception("the branch hasn't been found");

            data.ListBranch.Remove(tmp);
            AddBranch(branch);
        }
        /// <summary>
        /// Getting the list , or just special element
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Branch> ListOfBranch(Func<Branch, bool> predicate = null)
        {
            // no condition, return all the element
            if (predicate == null)
                return data.ListBranch.AsEnumerable();
            // return only special element
            return data.ListBranch.Where(predicate);
        }
        #endregion

        #region Dish's Functions
        /// <summary>
        /// Add dish function
        /// </summary>
        /// <param name="dish"></param>
        public void AddDish(Dish dish)
        {
            // verification first
            if (!data.ListDish.Where(d => d.Name.Equals(dish.Name) && d.SizeOfDish == dish.SizeOfDish && d.Request == dish.Request).Any())
            {
                // assigning new ID and adding
                dish.MenuID = CurrentDishID++;
                data.ListDish.Add(dish);
            }
            else
                throw new Exception("a dish with the name already exist");
        }
        /// <summary>
        /// Delete dish function
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteDish(int ID)
        {
            // checking if it's a valid id
            Dish tmp = data.ListDish.FirstOrDefault(d => d.MenuID == ID);
            if (tmp == null)
                throw new Exception("The dish doesn't exist anymore...");
            // checking if the current dish isn't order
            else if (data.ListOrderedDish.Where(d => d.DishID == ID).Any())
                throw new Exception("Can't delete this dish , he has been ordered");
            // remove
            else
                return data.ListDish.Remove(tmp);
        }
        /// <summary>
        /// Update dish function
        /// </summary>
        /// <param name="OldID"></param>
        /// <param name="dish"></param>
        public void UpdateDish(int OldID, Dish dish)
        {
            // checking the id
            Dish tmp = data.ListDish.Where(d => d.MenuID == OldID).First();
            if (tmp == null)
                throw new Exception("the dish doesn't exist");
            // remove the old object and adding object
            data.ListDish.Remove(tmp);
            AddDish(dish);
        }
        /// <summary>
        /// Getting the list , or just special element
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Dish> ListOfDish(Func<Dish, bool> predicate = null)
        {
            if (predicate == null)
                return data.ListDish.AsEnumerable();

            return data.ListDish.Where(predicate);
        }
        #endregion

        #region Ordered_dish's Functions
        /// <summary>
        /// Add function
        /// </summary>
        /// <param name="orderdish"></param>
        public void AddOrdered(Ordered_Dish orderdish)
        {
            // simply add the ordered dish
             data.ListOrderedDish.Add(orderdish);
            
        }
        /// <summary>
        /// Delete all the OrderedDish
        /// </summary>
        /// <param name="OrderNumber"></param>
        public void DeleteAllOrdered(int OrderNumber)
        {
            // first step delete all the ordered dish , utilisation of .ToArray() because 
            // we can only iterate an Ienumerable collection with a foreach , we can't do any modification on the list
            foreach (Ordered_Dish item in ListOfOrdered(x => x.OrderNumber == OrderNumber).ToArray())
            {
                // increase quantity no gamble
                IncreaseQuantityAvailable(item);
                // remove the item
                data.ListOrderedDish.Remove(item);
            }
            // remove the initial list
            DeleteOrder(OrderNumber);
        }
        /// <summary>
        /// Delete a specific OrderedDish
        /// </summary>
        /// <param name="OrderNumber"></param>
        /// <param name="DishID"></param>
        /// <param name="Quantity"></param>
        /// <returns></returns>
        public bool DeleteSpecificOrdered(int OrderNumber, int DishID, int Quantity)
        {
            // searching the first occurence
            Ordered_Dish tmp = data.ListOrderedDish.FirstOrDefault(o => o.OrderNumber == OrderNumber && o.DishID == DishID && o.QuantityOrder == Quantity);
            // avoid remove null object
            if (tmp == null)
                throw new Exception("this ordered doesn't exist, check your input data");

            IncreaseQuantityAvailable(tmp);
            // remove
            return data.ListOrderedDish.Remove(tmp);
            
        }
        /// <summary>
        /// Update an ordered dish
        /// </summary>
        /// <param name="orderdish"></param>
        public void UpdateOrdered(Ordered_Dish orderdish)
        {
            // searching
            int index = data.ListOrderedDish.FindIndex(o => o.OrderNumber == orderdish.OrderNumber);
            // no matching
            if (index == -1)
                throw new Exception("the ordered-dish hasn't been found");
            // update data
            data.ListOrderedDish[index] = orderdish;
        }
        public void IncreaseQuantityAvailable(Ordered_Dish dish)
        {
            // searching
            Dish tmp = ListOfDish(d => d.MenuID == dish.DishID).FirstOrDefault();
            // no matching
            if (tmp == null)
                throw new Exception("unable to find this dish");
            // update quantity
            tmp.Quantity += dish.QuantityOrder;
        }
        /// <summary>
        /// Getting the element of all the list or just element with condtion
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Ordered_Dish> ListOfOrdered(Func<Ordered_Dish, bool> predicate = null)
        {
           // no condition
            if (predicate == null)
                return data.ListOrderedDish.AsEnumerable();
            // with condition
            return data.ListOrderedDish.Where(predicate);
        }
        #endregion

        #region Client's Function
        /// <summary>
        /// Return all the list , or special element
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Client> ListOfClient(Func<Client, bool> predicate = null)
        {
            // return all
            if (predicate == null)
                return data.ListClient.AsEnumerable();

            // return special element
            return data.ListClient.Where(predicate);
        }
        /// <summary>
        /// Searching client by address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public Client SearchTheClient(string address)
        {
            // searching
            Client tmp = data.ListClient.FirstOrDefault(c => c.Address == address);
            // no matching
            if (tmp == null)
                throw new Exception("the client doesn't exist");
            // return client
            else
                return tmp;
        }
        /// <summary>
        /// Add client to the list
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(Client client)
        {
            // searching by the credit card and adress (only one by person)
            Client tmp = data.ListClient.FirstOrDefault(c => c.Address == client.Address && c.CreditCardNumber == client.CreditCardNumber);
            // no matching so add to the list
            if (tmp == null)
            {
                client.ClientID = CurrentClientID++;
                data.ListClient.Add(client);
            }
            // matching
            else
                throw new Exception("Your already reference in our list");
        }
        #endregion
    }
}
