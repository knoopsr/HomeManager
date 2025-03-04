using HomeManager.Common;
using HomeManager.Model.Personen;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DataService.Personen
{
    public interface INotitiesDataService : IDataService<clsNotitiesModel>
    {
        ObservableCollection<clsNotitiesModel> GetByPersoonID(int id);
    }
}
