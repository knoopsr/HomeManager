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
        public bool Delete(clsTelefoonTypeM entity)
        {
            return Repo.Delete(entity);
        }

        public clsTelefoonTypeM Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsTelefoonTypeM> GetAll()
        {
            return Repo.GetAll();
        }

        public clsTelefoonTypeM GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsTelefoonTypeM GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert(clsTelefoonTypeM entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsTelefoonTypeM entity)
        {
            return Repo.Update(entity);
        }
    }
}
