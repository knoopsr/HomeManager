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
    public class clsProvincieDataService : IProvincieDataService
    {
        IProvincieRepository Repo = new clsProvincieRepository();
        
        public bool Delete(clsProvincieModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsProvincieModel Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsProvincieModel> GetAll()
        {
            return Repo.GetAll();
        }

        public clsProvincieModel GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsProvincieModel GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert(clsProvincieModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsProvincieModel entity)
        {
            return Repo.Update(entity);
        }
    }
}
