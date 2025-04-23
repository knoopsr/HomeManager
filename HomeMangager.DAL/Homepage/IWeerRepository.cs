using HomeManager.Common;
using HomeManager.Model.Homepage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.Homepage
{
    public interface IWeerRepository : IRepository<clsWeerModel>
    {
        ObservableCollection<clsWeerModel> GetWeerData(string gemeente);
        string GetGemeenteByAccountID(int accountId);
    }
}
