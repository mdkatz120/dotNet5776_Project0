// Simon Moyal 1177707
//David Katz 065970394
using System;

namespace BE
{
    /// <summary>
    /// Class client, all the information of a client are keep there
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Age of client 
        /// </summary>
        private int Age;
        public int age
        {
            // basic verification
            set
            {
                if (value > 0 && value < 120)
                    Age = value;

            }
            get
            {
                return Age;
            }
        }
        /// <summary>
        /// credit card number
        /// </summary>
        private int creditCardNumber;
        public int CreditCardNumber
        {
            set
            {
                creditCardNumber = value;
            }
            get
            {
                return creditCardNumber;
            }
        }
        /// <summary>
        /// The id of the client in the restaurant, in order in the futur , have some reduction
        /// </summary>
        public int ClientID { set; get; }
        /// <summary>
        /// address
        /// </summary>
        public string Address { set; get; }
        /// <summary>
        /// adress for delivery
        /// </summary>
        public string AddressForDelivery { set; get; }
        /// <summary>
        /// name of the client
        /// </summary>
        public string ClientName { set; get; }
        /// <summary>
        /// number telephone
        /// </summary>
        public int NumberTelephone { set; get; }
        /// <summary>
        /// Override ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Name: {0}, Age: {1}, Adress: {2}, Telephone: {3}", ClientName, age, Address, NumberTelephone);
        }
    }
}
