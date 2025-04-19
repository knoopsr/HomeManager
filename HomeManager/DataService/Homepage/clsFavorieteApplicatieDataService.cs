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
    public class clsFavorieteApplicatieDataService : IFavorieteApplicatieDataServive
    {
        IFavorieteApplicatieRepository Repo = new clsFavorieteApplicatieRepository();
        public bool Delete(clsFavorieteApplicatieModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsFavorieteApplicatieModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsFavorieteApplicatieModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsFavorieteApplicatieModel> GetByAccountId(int accountId)
        {
            return Repo.GetByAccountId(accountId);
        }

        public clsFavorieteApplicatieModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public clsFavorieteApplicatieModel GetFirst()
        {
            throw new NotImplementedException();
        }

        public bool Insert(clsFavorieteApplicatieModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsFavorieteApplicatieModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
