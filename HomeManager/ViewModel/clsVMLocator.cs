using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.ViewModel
{
    public class clsVMLocator
    {
        public clsCategorieViewModel CategorieViewModel
        {
            get
            {
                return new clsCategorieViewModel(); ;
            }
        }
    }
}
