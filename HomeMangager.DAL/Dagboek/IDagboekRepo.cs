using HomeManager.Common;
using HomeManager.Model.Dagboek;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.Dagboek
{
    public interface IDagboekRepo : IRepository<clsDagboekModel>
    {
        ObservableCollection<clsDagboekModel> GetAllByPersoonID(int persoonID);
    }
}
