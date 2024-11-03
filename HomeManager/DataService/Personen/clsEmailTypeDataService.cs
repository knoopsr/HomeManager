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
    public class clsEmailTypeDataService : IEmailTypeDataService
    {
        IEmailTypeRepository Repo = new clsEmailTypeRepository();

        public bool Delete(clsEmailTypeM entity)
        {
            return Repo.Delete(entity);
        }

        public clsEmailTypeM Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsEmailTypeM> GetAll()
        {
            return Repo.GetAll();
        }

        public clsEmailTypeM GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsEmailTypeM GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert(clsEmailTypeM entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsEmailTypeM entity)
        {
            return Repo.Update(entity);
        }
    }
}
