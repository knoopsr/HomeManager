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
    public class clsGemeenteDataService : IGemeenteDataService
    {
        IGemeenteRepository Repo = new clsGemeenteRepository();
        public bool Delete(clsGemeenteM entity)
        {
            return Repo.Delete(entity);
        }

        public clsGemeenteM Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsGemeenteM> GetAll()
        {
            return Repo.GetAll();
        }

        public clsGemeenteM GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsGemeenteM GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert(clsGemeenteM entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsGemeenteM entity)
        {
            return Repo.Update(entity);
        }
    }
}
