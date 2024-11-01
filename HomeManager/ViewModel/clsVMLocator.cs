using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.ViewModel
{
    public class clsVMLocator
    {
        public clsPersoonVM PersoonViewModel
        {
            get
            {
                return new clsPersoonVM();
            }
        }
    }
}
