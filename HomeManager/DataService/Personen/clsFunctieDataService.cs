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
    public class clsFunctieDataService : IFunctieDataService
    {
        IFunctiesRepository Repo = new clsFunctiesRepository();
        public bool Delete(clsFunctiesModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsFunctiesModel Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsFunctiesModel> GetAll()
        {
            return Repo.GetAll();
        }

        public clsFunctiesModel GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsFunctiesModel GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert(clsFunctiesModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsFunctiesModel entity)
        {
            return Repo.Update(entity);
        }
    }
}
