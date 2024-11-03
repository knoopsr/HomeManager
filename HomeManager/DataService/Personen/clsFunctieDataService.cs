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
        public bool Delete(clsFunctiesM entity)
        {
            return Repo.Delete(entity);
        }

        public clsFunctiesM Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsFunctiesM> GetAll()
        {
            return Repo.GetAll();
        }

        public clsFunctiesM GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsFunctiesM GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert(clsFunctiesM entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsFunctiesM entity)
        {
            return Repo.Update(entity);
        }
    }
}
