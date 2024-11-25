using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Model.Todo;
using HomeManager.ViewModel;

namespace HomeManager.ViewModel
{
    public class clsVMLocator
    {
        public clsCollectiesVM CollectiesViewModel => new clsCollectiesVM();
        public clsCategorieënVM CategorieënViewModel => new clsCategorieënVM();
        public clsKleurenVM KleurenViewModel => new clsKleurenVM();
    }
}
