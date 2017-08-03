// Simon Moyal 1177707
//David Katz 065970394
using System;

namespace BE
{
    /// <summary>
    /// enum of cashrout
    /// </summary>
    public enum CasheRoutLevel
    {
        Low, Medium, Hight
    };
    /// <summary>
    /// Class Order
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Datetime parameter
        /// </summary>
        public DateTime Date { set; get; }
        /// <summary>
        /// each order has a single numberorder
        /// </summary>
        private int NumberOrder;
        public int OrderNumber
        {
            set
            {
                // verification
                string tmp = value.ToString();
                if (tmp.Length == 8)
                    NumberOrder = value;
            }
            get
            {
                return NumberOrder;
            }
        }
        /// <summary>
        /// Branch mispar field, in order to know in wich branch we have mad the order
        /// </summary>
        public int BranchMispar { set; get; }
        /// <summary>
        /// Number of delivery man field
        /// </summary>
        public int NumberOfDeliveryManAvailable { set; get; }
        /// <summary>
        /// object of our cashroutlevel enum
        /// </summary>
        public CasheRoutLevel choice { set; get; }
        /// <summary>
        /// Override of ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("OrderNumber: {0}, CashRout Selected: {1}", OrderNumber, choice);
        }
    }
}
