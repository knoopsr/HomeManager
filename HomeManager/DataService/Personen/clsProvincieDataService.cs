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
        
        public bool Delete(clsProvincieM entity)
        {
            return Repo.Delete(entity);
        }

        public clsProvincieM Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsProvincieM> GetAll()
        {
            return Repo.GetAll();
        }

        public clsProvincieM GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsProvincieM GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert(clsProvincieM entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsProvincieM entity)
        {
            return Repo.Insert(entity);
        }
    }
}
