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
    public class clsFotoCarouselDataService : IFotoCarouselDataService
    {
        IFotoCarouselRepository Repo = new clsFotoCarouselRepository();
        public bool Delete(clsFotoCarouselModel entity)
        {
            throw new NotImplementedException();
        }

        public clsFotoCarouselModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsFotoCarouselModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsFotoCarouselModel> GetByAccountId(int accountId)
        {
            return Repo.GetByAccountId(accountId);
        }

        public clsFotoCarouselModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public clsFotoCarouselModel GetFirst()
        {
            throw new NotImplementedException();
        }

        public bool Insert(clsFotoCarouselModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsFotoCarouselModel entity)
        {
            return Repo.Update(entity);
        }
    }
}
