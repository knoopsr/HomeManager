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
    public class clsLandDataService : ILandDataService
    {
        ILandRepository Repo = new clsLandRepository();
        public bool Delete(clsLandModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsLandModel Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsLandModel> GetAll()
        {
            return Repo.GetAll();
        }

        public clsLandModel GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsLandModel GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert(clsLandModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsLandModel entity)
        {
            return Repo.Update(entity);
        }
    }
}
