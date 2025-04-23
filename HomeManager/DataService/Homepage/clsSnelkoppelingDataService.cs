using HomeManager.DAL.Homepage;
using HomeManager.Model.Homepage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DataService.Homepage
{
    public class clsSnelkoppelingDataService : ISnelkoppelingDataService
    {
        ISnelkoppelingRepository Repo = new clsSnelkoppelingRepository();
        public bool Delete(clsSnelkoppelingModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsSnelkoppelingModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsSnelkoppelingModel> GetAll()
        {
            return Repo.GetAll();
        }

        public ObservableCollection<clsSnelkoppelingModel> GetByAccountId(int accountId)
        {
            return Repo.GetByAccountId(accountId);
        }

        public clsSnelkoppelingModel GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsSnelkoppelingModel GetFirst()
        {
            throw new NotImplementedException();
        }

        public bool Insert(clsSnelkoppelingModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsSnelkoppelingModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
