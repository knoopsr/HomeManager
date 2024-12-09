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
    public class clsAdressenDataService : IAdressenDataService
    {
        IAdressenRepository Repo = new clsAdressenRepository();
        public bool Delete(clsAdressenModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsAdressenModel Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsAdressenModel> GetAll()
        {
            return Repo.GetAll();
        }

        public clsAdressenModel GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsAdressenModel GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert(clsAdressenModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsAdressenModel entity)
        {
            return !Repo.Update(entity);
        }
    }
}
