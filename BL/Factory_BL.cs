// Simon Moyal 1177707
//David Katz 065970394

namespace BL
{
    public class Factory_BL
    {
        static IBL bl = null;
        // getting the bl
        public static IBL GetBL()
        {
            // singleton
            if (bl == null)
                bl = new BL.IBL_imp();

            return bl;
        }
    }
}
