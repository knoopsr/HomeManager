
using HomeManager.DAL.Dagboek;
using HomeManager.Model.Dagboek;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DataService.Dagboek
{
    public class clsDagboekDataService : IDagboekDataService
    {
        private IDagboekRepository myRepo = new clsDagboekRepository();

        public bool Delete(clsDagboekModel entity)
        {
            return myRepo.Delete(entity);
        }

        public ObservableCollection<clsDagboekModel> GetAllByPersoonID(int persoonID)
        {
            return myRepo.GetAllByPersoonID( persoonID );
        }

        public bool Insert(clsDagboekModel entity)
        {
            return myRepo.Insert(entity);
        }

        public bool Update(clsDagboekModel entity)
        {
            return myRepo.Update(entity);
        }

        #region notImplemented
        public clsDagboekModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsDagboekModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public clsDagboekModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public clsDagboekModel GetFirst()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
