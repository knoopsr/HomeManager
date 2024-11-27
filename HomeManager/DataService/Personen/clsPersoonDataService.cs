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
    public class clsPersoonDataService : IPersoonDataService
    {
        IPersoonRepository Repo = new clsPersoonRepository();
        public bool Delete(clsPersoonM entity)
        {
            return Repo.Delete(entity);
        }



        public clsPersoonM Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsPersoonM> GetAll()
        {
            return Repo.GetAll();
        }

        public ObservableCollection<clsPersoonM> GetAllApplicationUser()
        {
            return Repo.GetAllApplicationUser();
        }

        public clsPersoonM GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsPersoonM GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert(clsPersoonM entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsPersoonM entity)
        {
            return Repo.Update(entity);
        }
    }
}
