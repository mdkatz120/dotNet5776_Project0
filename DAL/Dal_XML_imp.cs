// Simon Moyal 1177707
//David Katz 065970394
using System;
using System.Xml.Linq;
using System.IO;
using System.Linq;
using BE;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;

namespace DAL
{
    /// <summary>
    /// idal implementation, with XML
    /// </summary>
    public class Dal_XML_imp : idal
    {
        // usefull field
        public static int CurrentDishID, CurrentBranchNumber, CurrentClientID;
        public static int CurrentOrderID;

        // declaring all the Xelement foreach class and their file

        // dish section
        XElement dishRoot;
        string dishPath = @"dishXml";

        // order section
        XElement orderRoot;
        string OrderPath = @"orderXml";

        // ordereddish section
        XElement OrderedDishRoot;
        string OrderedPath = @"orderedXml";

        // branch section
        XElement BranchRoot;
        string BranchPath = @"branchXml";

        // client section
        XElement ClientRoot;
        string ClientPath = @"clientXml";

        /// <summary>
        /// Constructor , reading the file or create new one
        /// </summary>
        public Dal_XML_imp()
        {
            if (!File.Exists(dishPath))
            {
                CreateFiles();
            }
            else
                LoadData();
            // after we have load the data, getting the current id of all object
            FindNewID();
        }
        private void CreateFiles()
        {
            dishRoot = new XElement("dish");
            dishRoot.Save(dishPath);

            orderRoot = new XElement("order");
            orderRoot.Save(OrderPath);

            OrderedDishRoot = new XElement("ordered-dish");
            OrderedDishRoot.Save(OrderedPath);

            BranchRoot = new XElement("branch");
            BranchRoot.Save(BranchPath);

            ClientRoot = new XElement("client");
            ClientRoot.Save(ClientPath);
        }
        private void LoadData()
        {
            try
            {
                dishRoot = XElement.Load(dishPath);
                orderRoot = XElement.Load(OrderPath);
                OrderedDishRoot = XElement.Load(OrderedPath);
                BranchRoot = XElement.Load(BranchPath);
                ClientRoot = XElement.Load(ClientPath);
            }
            catch
            {
                throw new Exception("File not loaded properly");
            }
        }
        /// <summary>
        /// Each time we start the program we have to check what's the last number given
        /// to avoid double numberorder
        /// </summary>
        public void FindNewID()
        {
            // getting OrderID
            if (ListOfOrder().Count() == 0)
                CurrentOrderID = 10000000;
            else
                CurrentOrderID = ++ListOfOrder().Last().OrderNumber;

            // getting DishID
            if (ListOfDish().Count() == 0)
                CurrentDishID = 1;
            else
                CurrentDishID = ++ListOfDish().Last().MenuID;

            // getting BranchID
            if (ListOfBranch().Count() == 0)
                CurrentBranchNumber = 1;
            else
                CurrentBranchNumber = ++ListOfBranch().Last().BranchNumber;

            // getting ClientID
            if (ListOfClient().Count() == 0)
                CurrentClientID = 1;
            else
                CurrentClientID = ++ListOfClient().Last().ClientID;
        }

        #region Dish's Functions
        /// <summary>
        /// Add dish function
        /// </summary>
        /// <param name="dish"></param>
        public void AddDish(Dish dish)
        {
            // checking , avoid double
            if (!ListOfDish(d => d.Name.Equals(dish.Name) && d.SizeOfDish == dish.SizeOfDish && d.Request == dish.Request).Any())
            {
                // assigning new ID and adding
                dish.MenuID = CurrentDishID++;
                // copying the file for the image
                dish.Image = CopyFiles(dish.Image, dish.Name);
                // converting the dish to an xelement
                dishRoot.Add(FromDishToXelement(dish));
                // save the file
                dishRoot.Save(dishPath);
                
            }
            else
                throw new Exception("a dish with the name already exist");
        }
        /// <summary>
        /// CopyFiles function , copy the path of the image
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destinationName"></param>
        /// <returns></returns>
        private string CopyFiles(string sourcePath, string destinationName)
        {
            try
            {
                int postfixIndex = sourcePath.LastIndexOf('.');
                string postfix = sourcePath.Substring(postfixIndex);
                destinationName += postfix;

                string destinationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string destinationFullName = @"UI/Images\" + destinationName;

                System.IO.File.Copy(sourcePath, destinationPath + "\\" + destinationFullName, true);
                return destinationFullName;
            }
            // if doesn't found
            catch (Exception ex)
            {
                return @"UI/Images/default.jpg";
            }
        }
        /// <summary>
        /// Delete dish function
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteDish(int id)
        {
            // getting the dish
            XElement dish = (from item in dishRoot.Elements()
                                   where int.Parse(item.Element("MenuID").Value) == id
                                   select item).FirstOrDefault();
            // no matching
            if (dish == null)
                throw new Exception("the dish with this id doesn't exist");
            // removing from the xml
            dish.Remove();
            // save data
            dishRoot.Save(dishPath);
            return true;
        }
        /// <summary>
        /// Update dish function
        /// </summary>
        /// <param name="OldID"></param>
        /// <param name="dish"></param>
        public void UpdateDish(int OldID, Dish dish)
        {
            // getting the dish
            Dish tmp = GetDish(OldID);
            // no matching
            if (tmp == null)
                throw new Exception("Dish with the same id not exist");
            // deleting the old 
            DeleteDish(OldID);
            // adding the dish with new data
            AddDish(dish);
        }
        /// <summary>
        /// getting a dish by the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dish GetDish(int id)
        {

            XElement tmp = null;
            // searching
            try
            {
                tmp = (from item in dishRoot.Elements()
                       where int.Parse(item.Element("MenuID").Value) == id
                       select item).FirstOrDefault();
            }
            catch(Exception)
            {
                return null;
            }
            if (tmp == null)
                return null;
            // return the dish
            return FromXelementToDish(tmp);
         }
        /// <summary>
        /// Convert Xelement to dish
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public Dish FromXelementToDish(XElement element)
        {
            Dish tmp = new Dish();
            // boucle , while we haven't pass through all the property present in our object dish
            foreach (PropertyInfo item in typeof(Dish).GetProperties())
            {
                // getting and convert the value
                TypeConverter typeconverter = TypeDescriptor.GetConverter(item.PropertyType);
                object convertervalue = typeconverter.ConvertFromString(element.Element(item.Name).Value);

                // setting the value
                item.SetValue(tmp, convertervalue);
            }
            // return the object
            return tmp;
        }
        /// <summary>
        ///Convert dish object to a XElement
        /// </summary>
        /// <param name="dish"></param>
        /// <returns></returns>
        public XElement FromDishToXelement(Dish dish)
        {
            // create new XElement
            XElement DishXelement = new XElement("Dish");
            // while we haven't pass through all the property of dish
            foreach (PropertyInfo item in typeof(Dish).GetProperties())
            {
                // adding the element
                DishXelement.Add(new XElement(item.Name, item.GetValue(dish, null)));
            }
            // return the Xelement
            return DishXelement;
        }
        /// <summary>
        /// Getting all the dish
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Dish> ListOfDish(Func<Dish, bool> predicate = null)
        {
            // no specific search
            if (predicate == null)
            {
                // return all the element present in the file
                return from item in dishRoot.Elements()
                       select FromXelementToDish(item);
            }
            // returning specific element
           return from item in dishRoot.Elements()
                  let tmpdish = FromXelementToDish(item)
                  where predicate(tmpdish)
                  select tmpdish;
        }
        #endregion

        #region Ordered-Dish's Function
        /// <summary>
        /// Add an ordered dish function
        /// </summary>
        /// <param name="orderedish"></param>
        public void AddOrdered(Ordered_Dish orderedish)
        {
            // if empty , winning time
            if (ListOfOrdered().Count() == 0)
            {
                // add the element to the file , after the convertion to Xelement
                OrderedDishRoot.Add(FromOrderedToXelement(orderedish));
            }
            else
            {
                // searching if we haven't already have a command of the same dish 
                Ordered_Dish tmp = ListOfOrdered(x => x.OrderNumber == orderedish.OrderNumber && x.DishID == orderedish.DishID).FirstOrDefault();
                // if we have , adding quantity
                if (tmp != null)
                    tmp.QuantityOrder += orderedish.QuantityOrder;
                
                // adding a new xelement
                else
                    OrderedDishRoot.Add(FromOrderedToXelement(orderedish));
            }
            // reducing quantity available
            ReduceQuantityAvailable(orderedish);
            // saving the fike
            OrderedDishRoot.Save(OrderedPath);
        }
        /// <summary>
        /// Delete all the ordered by an ordernumber
        /// </summary>
        /// <param name="OrderNumber"></param>
        public void DeleteAllOrdered(int OrderNumber)
        {
            // checking first if we have some ordered-dish via the number order
            if (ListOfOrdered(x => x.OrderNumber == OrderNumber).Count() != 0)
            {
                // while remaining
                while (ListOfOrdered(x => x.OrderNumber == OrderNumber).Count() > 0)
                {
                    // getting a ordered
                    XElement tmp = (from item in OrderedDishRoot.Elements()
                                    where int.Parse(item.Element("OrderNumber").Value) == OrderNumber
                                    select item).First();
                    // increasing the stock , no gamble
                    IncreaseQuantityAvailable(FromXelementToOrdered(tmp));
                    // removing from the file
                    tmp.Remove();
                    // saving the file
                    OrderedDishRoot.Save(OrderedPath);
                }
            }
            // now deleting the order
            DeleteOrder(OrderNumber);
        }
        /// <summary>
        /// Deleting a specific ordered dish
        /// </summary>
        /// <param name="OrderNumber"></param>
        /// <param name="DishID"></param>
        /// <param name="Quantity"></param>
        /// <returns></returns>
        public bool DeleteSpecificOrdered(int OrderNumber, int DishID, int Quantity)
        {
            // searching if the delete request quantity is equal to the original one
            XElement Order = (from item in OrderedDishRoot.Elements()
                              where int.Parse(item.Element("OrderNumber").Value) == OrderNumber &&
                                    int.Parse(item.Element("DishID").Value) == DishID &&
                                    int.Parse(item.Element("QuantityOrder").Value) == Quantity
                              select item).FirstOrDefault();
            
            // searching if the delete request quantity is below to the original one
            XElement Order2 = (from item in OrderedDishRoot.Elements()
                              where int.Parse(item.Element("OrderNumber").Value) == OrderNumber &&
                                    int.Parse(item.Element("DishID").Value) == DishID &&
                                    int.Parse(item.Element("QuantityOrder").Value) > Quantity
                              select item).FirstOrDefault();

            // avoid remove null object
            if (Order == null && Order2 == null)
                throw new Exception("this ordered doesn't exist, check your input data");

            // we want to delete all the ordered-dish
            else if (Order != null)
            {
                // increase quantity
                IncreaseQuantityAvailable(FromXelementToOrdered(Order));
                // remove
                Order.Remove();
                OrderedDishRoot.Save(OrderedPath);
            }
            // reducing the quantity present in the XML
            if (Order2 != null)
            {
                // getting the value
                int tmp = int.Parse(Order2.Element("QuantityOrder").Value);
                // updating
                Order2.Element("QuantityOrder").Value = (tmp - Quantity).ToString();
                // increase quantity
                IncreaseQuantityAvailable(new Ordered_Dish {QuantityOrder = Quantity, DishID = DishID });
                // saving file
                OrderedDishRoot.Save(OrderedPath);
            }
            return true;
        }
        /// <summary>
        /// Increase quantity available for a dish
        /// </summary>
        /// <param name="dish"></param>
        public void IncreaseQuantityAvailable(Ordered_Dish dish)
        {
            // getting the dish
            XElement Order = (from item in dishRoot.Elements()
                              where int.Parse(item.Element("MenuID").Value) == dish.DishID
                              select item).FirstOrDefault();
            // no matching
            if (Order == null)
                throw new Exception("can't find the dish");

            // updating quantity remaining
            int tmp = int.Parse(Order.Element("Quantity").Value);
            Order.Element("Quantity").Value = (tmp + dish.QuantityOrder).ToString();
            // saving the file
            dishRoot.Save(dishPath);
        }
        /// <summary>
        /// Reduce function , reduce quantity after an ordered dish
        /// </summary>
        /// <param name="dish"></param>
        public void ReduceQuantityAvailable(Ordered_Dish dish)
        {
            // getting the dish
            XElement Order = (from item in dishRoot.Elements()
                              where int.Parse(item.Element("MenuID").Value) == dish.DishID
                              select item).FirstOrDefault();
            // no matching
            if (Order == null)
                throw new Exception("can't find the dish");

            // trying to reduce
            int tmp = int.Parse(Order.Element("Quantity").Value);
            // excpetion
            if (tmp - dish.QuantityOrder < 0)
                throw new Exception("the requested quantity is beyond our available stock !");
            // decreasing the value
            Order.Element("Quantity").Value = (tmp - dish.QuantityOrder).ToString();
            // saving the file
            dishRoot.Save(dishPath);
        }
        /// <summary>
        /// Convert Ordered-dish object to a XElement
        /// </summary>
        /// <param name="ordered"></param>
        /// <returns></returns>
        public XElement FromOrderedToXelement(Ordered_Dish ordered)
        {
            // create a new Xelement
            XElement Ordered = new XElement("ordered-dish");
            // while we haven't pass through all the property of ordered-dish
            foreach (PropertyInfo item in typeof(Ordered_Dish).GetProperties())
            {
                // adding the element
                Ordered.Add(new XElement(item.Name, item.GetValue(ordered, null)));
            }
            // return the new Xelement
            return Ordered;
        }
        /// <summary>
        /// Convert Xelement to an Ordered-dish's object
        /// </summary>
        /// <param name="ordered"></param>
        /// <returns></returns>
        public Ordered_Dish FromXelementToOrdered(XElement ordered)
        {
            Ordered_Dish tmp = new Ordered_Dish();
            // while we haven't pass through all the property of dish
            foreach (PropertyInfo item in typeof(Ordered_Dish).GetProperties())
            {
                // getting and convert the value
                TypeConverter typeconverter = TypeDescriptor.GetConverter(item.PropertyType);
                object convertervalue = typeconverter.ConvertFromString(ordered.Element(item.Name).Value);
                
                // setting the value
                item.SetValue(tmp, convertervalue);
            }
            // return the ordered-dish
            return tmp;
        }
        /// <summary>
        /// Getting all The ordered-dish
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Ordered_Dish> ListOfOrdered(Func<Ordered_Dish, bool> predicate = null)
        {
            // no specific search
            if (predicate == null)
            {
                // return all the element present in the file
                return from item in OrderedDishRoot.Elements()
                       select FromXelementToOrdered(item);
            }
            // returning specific element
            return from item in OrderedDishRoot.Elements()
                   let tmp = FromXelementToOrdered(item)
                   where predicate(tmp)
                   select tmp;
        }
        #endregion

        #region Branch
        /// <summary>
        /// Adding a new branch
        /// </summary>
        /// <param name="branch"></param>
        public void AddBranch(Branch branch)
        {
            // first verification if we already have this branch number or number telephone (both can't have double)
            if (!ListOfBranch(b => b.BranchNumberTelephone == branch.BranchNumberTelephone || (b.BranchName == branch.BranchName && b.CashroutLevel == branch.CashroutLevel)).Any())
            {
                // assigning new ID
                branch.BranchNumber = CurrentBranchNumber++;
                // adding the new branch after converting into XElment object
                BranchRoot.Add(FromBranchToXelement(branch));
                // saving
                BranchRoot.Save(BranchPath);
            }
            else
                throw new Exception("a branch with the same ID/Number telephone already exist");
        }
        /// <summary>
        /// Deleting a branch via it's BranchID
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public bool DeleteBranch(int Number)
        {
            // getting the branch
            XElement branch  = (from item in dishRoot.Elements()
                             where int.Parse(item.Element("BranchNumber").Value) == Number
                             select item).FirstOrDefault();
            // no matching
            if (branch == null)
                throw new Exception("the dish with this id doesn't exist");
            // removing the branch
            branch.Remove();
            // saving
            dishRoot.Save(dishPath);
            return true;
        }
        /// <summary>
        /// Update the branch
        /// </summary>
        /// <param name="oldnumber"></param>
        /// <param name="branch"></param>
        public void UpdateBranch(int oldnumber, Branch branch)
        {
            // getting the branch
            Branch tmp = GetBranch(oldnumber);
            // no matching
            if (tmp == null)
                throw new Exception("Dish with the same id not exist");
            // deleting the old branch
            DeleteBranch(tmp.BranchNumber);
            // adding the new one
            AddBranch(branch);
        }
        /// <summary>
        /// Convert a branch to Xelement
        /// </summary>
        /// <param name="branch"></param>
        /// <returns></returns>
        public XElement FromBranchToXelement(Branch branch)
        {
            XElement Branch = new XElement("Branch");
            // while we haven't pass through all the property of dish
            foreach (PropertyInfo item in typeof(Branch).GetProperties())
            {
                // adding the element
                Branch.Add(new XElement(item.Name, item.GetValue(branch, null)));
            }
            // return the new Xelement
            return Branch;
        }
        /// <summary>
        /// Getting a brach via the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Branch GetBranch(int id)
        {
            XElement tmp = null;
            try
            {
                //getting the branch
                tmp = (from item in dishRoot.Elements()
                       where int.Parse(item.Element("BranchNumber").Value) == id
                       select item).FirstOrDefault();
            }
            // NULL Exception
            catch (Exception)
            {
                return null;
            }
            // return the brach
            return FromXelementToBranch(tmp);
        }
        /// <summary>
        /// Convert a XElement to Branch
        /// </summary>
        /// <param name="branch"></param>
        /// <returns></returns>
        public Branch FromXelementToBranch(XElement branch)
        {
            Branch tmp = new Branch();
            // while we haven't pass through all the property of dish
            foreach (PropertyInfo item in typeof(Branch).GetProperties())
            {
                // getting and convert the value
                TypeConverter typeconverter = TypeDescriptor.GetConverter(item.PropertyType);
                object convertervalue = typeconverter.ConvertFromString(branch.Element(item.Name).Value);

                //setting the value
                item.SetValue(tmp, convertervalue);
            }
            // return the branch
            return tmp;
        }
        /// <summary>
        /// Getting all the Branch
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Branch> ListOfBranch(Func<Branch, bool> predicate = null)
        {
            // no specific search
            if (predicate == null)
                // return all the element present in the file
                return from item in BranchRoot.Elements()
                       select FromXelementToBranch(item);

            // returning specific element
            return from item in BranchRoot.Elements()
                   let tmp = FromXelementToBranch(item)
                   where predicate(tmp)
                   select tmp;
        }
        #endregion

        #region Order
        /// <summary>
        /// Add an Order
        /// </summary>
        /// <param name="order"></param>
        public void AddOrder(Order order)
        {
            // assinging orderID
            order.OrderNumber = CurrentOrderID++;
            // adding to the xml file after converting to xelement
            orderRoot.Add(FromOrderToXelement(order));
            // saving the file
            orderRoot.Save(OrderPath);
        }
        /// <summary>
        /// Delete an order via the numberorder
        /// </summary>
        /// <param name="NumberOrder"></param>
        /// <returns></returns>
        public bool DeleteOrder(int NumberOrder)
        {
            // getting the order
            XElement order = (from item in orderRoot.Elements()
                               where int.Parse(item.Element("OrderNumber").Value) == NumberOrder
                               select item).FirstOrDefault();
            // no matching
            if (order == null)
                throw new Exception("the dish with this id doesn't exist");
            // remove the xelement from the XML
            order.Remove();
            // saving path
            dishRoot.Save(dishPath);
            return true;
        }
        /// <summary>
        /// Convert Order object to XElement
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public XElement FromOrderToXelement(Order order)
        {
            // creating new Xelement
            XElement Order = new XElement("Order");
            // while we haven't pass through all the property of dish
            foreach (PropertyInfo item in typeof(Order).GetProperties())
            {
                // adding the element
                Order.Add(new XElement(item.Name, item.GetValue(order, null)));
            }
            // return the Xelement
            return Order;
        }
        /// <summary>
        /// Convert XElement to Order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public Order FromXelementToOrder(XElement order)
        {
            Order tmp = new Order();
            // while we haven't pass through all the property of dish
            foreach (PropertyInfo item in typeof(Order).GetProperties())
            {
                // getting and convert the value
                TypeConverter typeconverter = TypeDescriptor.GetConverter(item.PropertyType);
                object convertervalue = typeconverter.ConvertFromString(order.Element(item.Name).Value);

                //setting the value
                item.SetValue(tmp, convertervalue);
            }
            // return the branch
            return tmp;
        }
        /// <summary>
        /// Getting all the order
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Order> ListOfOrder(Func<Order, bool> predicate = null)
        {
            // no specific search
            if (predicate == null)
                // return all the element present in the file
                return from item in orderRoot.Elements()
                       select FromXelementToOrder(item);

            // returning specific element
            return from item in orderRoot.Elements()
                   let tmp = FromXelementToOrder(item)
                   where predicate(tmp)
                   select tmp;
        }
        #endregion

        #region Client
        /// <summary>
        /// Search a client via it's address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public Client SearchTheClient(string address)
        {
            // searching
            XElement client = (from item in dishRoot.Elements()
                               where item.Element("Address").Value == address
                               select item).FirstOrDefault();
            // no matching
            if (client == null)
                throw new Exception("No matching client with this address");

            // return the client
            return FromXelementToClient(client);
        }
        /// <summary>
        /// Adding a client
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(Client client)
        {
            // checking first if the client isn't on our file before to add it
            if (ListOfClient(c => c.Address == client.Address && c.CreditCardNumber == client.CreditCardNumber).Count() == 0)
            {
                // assigning ClientID
                client.ClientID = CurrentClientID++;
                // adding to the XML file
                ClientRoot.Add(FromClientToXelement(client));
                // saving
                ClientRoot.Save(ClientPath);
            }
        }
        /// <summary>
        /// Convert Client to XELement
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public XElement FromClientToXelement(Client client)
        {
            // creating new Xelement
            XElement Client = new XElement("Client");
            // while we haven't pass through all the property of dish
            foreach (PropertyInfo item in typeof(Client).GetProperties())
            {
                // adding the element
                Client.Add(new XElement(item.Name, item.GetValue(client, null)));
            }
            // return the client
            return Client;
        }
        /// <summary>
        /// Convert XElement to Client
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public Client FromXelementToClient(XElement client)
        {
            Client tmp = new Client();
            // while we haven't pass through all the property of dish
            foreach (PropertyInfo item in typeof(Client).GetProperties())
            {
                // getting and convert the value
                TypeConverter typeconverter = TypeDescriptor.GetConverter(item.PropertyType);
                object convertervalue = typeconverter.ConvertFromString(client.Element(item.Name).Value);

                // setting the value
                item.SetValue(tmp, convertervalue);
            }
            // return the client
            return tmp;
        }
        /// <summary>
        /// Get all the client
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Client> ListOfClient(Func<Client, bool> predicate = null)
        {
            // no specific search
            if (predicate == null)
                // return all the element present in the file
                return from item in ClientRoot.Elements()
                       select FromXelementToClient(item);

            // returning specific element
            return from item in ClientRoot.Elements()
                   let tmp = FromXelementToClient(item)
                   where predicate(tmp)
                   select tmp;
        }
        #endregion
    }
}
