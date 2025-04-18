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
    public class clsFavorieteVensterDataService : IFavorieteVensterDataService
    {
        IFavorieteVensterRepository Repo = new clsFavorieteVensterRepository();
        public bool Delete(clsFavorieteVensterModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsFavorieteVensterModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsFavorieteVensterModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsFavorieteVensterModel> GetByAccountId(int accountId)
        {
            return Repo.GetByAccountId(accountId);
        }

        public clsFavorieteVensterModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public clsFavorieteVensterModel GetFirst()
        {
            throw new NotImplementedException();
        }

        public bool Insert(clsFavorieteVensterModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsFavorieteVensterModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
