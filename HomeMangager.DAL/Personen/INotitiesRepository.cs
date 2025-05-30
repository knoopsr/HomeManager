﻿using HomeManager.Common;
using HomeManager.Model.Personen;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.Personen
{
    public interface INotitiesRepository : IRepository<clsNotitiesModel>
    {
        ObservableCollection<clsNotitiesModel> GetByPersoonID(int id);
    }
}
