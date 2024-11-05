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
        public bool Delete(clsLandM entity)
        {
            return Repo.Delete(entity);
        }

        public clsLandM Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsLandM> GetAll()
        {
            return Repo.GetAll();
        }

        public clsLandM GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsLandM GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert(clsLandM entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsLandM entity)
        {
            return Repo.Update(entity);
        }
    }
}
