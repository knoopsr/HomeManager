using HomeManager.Common;
using HomeManager.Model.Dagboek;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DataService.Dagboek
{
    public interface IDagboekDataService : IDataService<clsDagboekModel>
    {
        ObservableCollection<clsDagboekModel> GetAllByPersoonID(string persoonID);
    }
}
