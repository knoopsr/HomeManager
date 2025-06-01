using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Common;
using HomeManager.Model.Dagboek;

namespace HomeManager.DAL.Dagboek
{
    public interface IDagboekRepository : IRepository<clsDagboekModel>
    {
        ObservableCollection<clsDagboekModel> GetAllByPersoonID(string persoonID);
    }
}
