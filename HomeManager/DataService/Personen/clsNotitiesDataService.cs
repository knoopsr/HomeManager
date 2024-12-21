using HomeManager.DAL.Personen;
using HomeManager.Model.Personen;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DataService.Personen
{
    public class clsNotitiesDataService : INotitiesDataService
    {
        INotitiesRepository Repo = new clsNotitiesRepository();

        public bool Delete(clsNotitiesModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsNotitiesModel Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsNotitiesModel> GetAll()
        {
            return Repo.GetAll();
        }

        public clsNotitiesModel GetById(int id)
        {
            return Repo.GetById(id);
        }

        public ObservableCollection<clsNotitiesModel> GetByPersoonID(int id)
        {
            return Repo.GetByPersoonID(id);
        }

        public clsNotitiesModel GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert(clsNotitiesModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsNotitiesModel entity)
        {
            return Repo.Update(entity);
        }
    }
}