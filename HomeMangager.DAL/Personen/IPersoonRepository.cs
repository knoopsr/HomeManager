using HomeManager.Common;
using HomeManager.DAL;
using HomeManager.Model.Personen;
using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.Personen
{
    public interface IPersoonRepository : IRepository<clsPersoonModel>
    {
        ObservableCollection<clsPersoonM> GetAllApplicationUser();
    }
}
