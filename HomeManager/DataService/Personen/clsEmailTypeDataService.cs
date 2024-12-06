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

        public bool Delete(clsEmailTypeModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsEmailTypeModel Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsEmailTypeModel> GetAll()
        {
            return Repo.GetAll();
        }

        public clsEmailTypeModel GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsEmailTypeModel GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert(clsEmailTypeModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsEmailTypeModel entity)
        {
            return Repo.Update(entity);
        }
    }
}
