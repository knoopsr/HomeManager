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
    public class clsTelefoonNummersDataService : ITelefoonNummersDataService
    {
        ITelefoonNummersRepository Repo = new clsTelefoonNummersRepository();
        public bool Delete(clsTelefoonNummersModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsTelefoonNummersModel Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsTelefoonNummersModel> GetAll()
        {
            return Repo.GetAll();
        }

        public clsTelefoonNummersModel GetById(int id)
        {
            return Repo.GetById(id);
        }

        public ObservableCollection<clsTelefoonNummersModel> GetByPersoonID(int id)
        {
            return Repo.GetByPersoonID(id);
        }

        public clsTelefoonNummersModel GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert(clsTelefoonNummersModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsTelefoonNummersModel entity)
        {
            return Repo.Update(entity);
        }
    }
}
