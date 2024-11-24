using HomeManager.DAL.Dagboek;
using HomeManager.Model.Dagboek;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Services
{
    public class clsDagboekDataService : IDagboekDataService
    {
        private IDagboekRepo Repo = new clsDagboekRepo();

        public bool Delete(clsDagboekModel entity)
        {
            return Repo.Delete(entity);
        }

        public ObservableCollection<clsDagboekModel> GetAllByPersoonID(int persoonID)
        {
            return Repo.GetAllByPersoonID(persoonID);
        }

        public bool Insert(clsDagboekModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsDagboekModel entity)
        {
            return Repo.Update(entity);
        }

        #region NotImplemented
        public clsDagboekModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public clsDagboekModel GetFirst()
        {
            throw new NotImplementedException();
        }

        public clsDagboekModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsDagboekModel> GetAll()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
