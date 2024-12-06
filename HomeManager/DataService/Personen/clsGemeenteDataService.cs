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
        public bool Delete(clsGemeenteModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsGemeenteModel Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsGemeenteModel> GetAll()
        {
            return Repo.GetAll();
        }

        public clsGemeenteModel GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsGemeenteModel GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert(clsGemeenteModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsGemeenteModel entity)
        {
            return Repo.Update(entity);
        }
    }
}
