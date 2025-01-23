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
        public bool Delete(clsPersoonModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsPersoonModel Find()


        public clsPersoonM Find()
        {
            return Repo.Find();
        }

        {
            return Repo.GetAll();
        }

        {
            return Repo.GetAllApplicationUser();
        }

        {
            return Repo.GetById(id);
        }

        {
            return Repo.GetFirst();
        }

        {
            return Repo.Insert(entity);
        }

        {
            return Repo.Update(entity);
        }
    }
}
