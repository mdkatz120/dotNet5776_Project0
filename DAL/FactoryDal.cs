using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FactoryDal
    {
        public static idal GetDAl()
        {
            return new Dal_XML_imp();
        }
    }
}
