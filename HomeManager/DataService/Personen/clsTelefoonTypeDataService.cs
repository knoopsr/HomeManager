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
    public class clsTelefoonTypeDataService : ITelefoonTypeDataService
    {
        ITelefoonTypeRepository Repo = new clsTelefoonTypeRepository();
        public bool Delete(clsTelefoonTypeModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsTelefoonTypeModel Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsTelefoonTypeModel> GetAll()
        {
            return Repo.GetAll();
        }

        public clsTelefoonTypeModel GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsTelefoonTypeModel GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert(clsTelefoonTypeModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsTelefoonTypeModel entity)
        {
            return Repo.Update(entity);
        }
    }
}
