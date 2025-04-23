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
    public class clsWeerDataService : IWeerDataService
    {
        private readonly IWeerRepository Repo = new clsWeerRepository();
        public bool Delete(clsWeerModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsWeerModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsWeerModel> GetAll()
        {
            return Repo.GetAll();
        }

        public clsWeerModel GetByAccountId(int accountId)
        {
            return Repo.GetById(accountId);
        }

        public clsWeerModel GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsWeerModel GetFirst()
        {
            throw new NotImplementedException();
        }

        public bool Insert(clsWeerModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsWeerModel entity)
        {
            return Repo.Update(entity);
        }
    }
}
