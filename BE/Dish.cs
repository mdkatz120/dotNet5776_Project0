// Simon Moyal 1177707
//David Katz 065970394
namespace BE
{
    /// <summary>
    /// Enum size, who represent the size of a dish 
    /// </summary>
    public enum size
    {
        low, medium, hight
    };
    public class Dish
    {
        /// <summary>
        /// Identificating number of the dish
        /// </summary>
        public int MenuID { set; get; }
        private int quantity;
        /// <summary>
        /// Property to know how many dish we can makes
        /// </summary>
        public int Quantity
        {
            set
            {
                quantity = value;
            }
            get
            {
                return quantity;
            }
        }
        /// <summary>
        /// Price of the dish
        /// </summary>
        public double PriceOfDish { set; get; }
        /// <summary>
        /// Name of the menu
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// Are the Dish available
        /// </summary>
        private bool Available;
        public bool available
        {
            set
            {
                Available = Quantity > 0;
            }
            get
            {
                return Available;
            }
        }
        /// <summary>
        /// CasheRoutLevel field , cashrout of the food
        /// </summary>
        public CasheRoutLevel Request { set; get; }
        /// <summary>
        /// Size field , size available
        /// </summary>
        public size SizeOfDish { set; get; }
        /// <summary>
        /// Preparation time of a dish
        /// </summary>
        public int PreparationTime { set; get; }
        /// <summary>
        /// we use a string to hold the path of the image
        /// </summary>
        private string image;
        public string Image
        {
            set
            {
                image = value;
            }
            get
            {
                return image;
            }
        }
        /// <summary>
        /// Override of ToString(), summary of the dish
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Available)
                return string.Format("Name: {0}, Price: {2}, Size: {1}, ID: {3}, QuantityAvailable: {4}", Name, SizeOfDish, PriceOfDish, MenuID, Quantity);
            else
                return "";
        }
    }
}


