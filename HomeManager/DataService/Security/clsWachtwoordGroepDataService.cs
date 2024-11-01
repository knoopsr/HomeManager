using HomeManager.DAL.Security;
using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DataService.Security
{
    public class clsWachtwoordGroepDataService : IWachtwoordGroepDataService
    {
        IWachtWoordGroepRepository _repo = new clsWachtWoordGroepRepository();
        public bool Delete(clsWachtWoordGroepModel entity)
        {
            return _repo.Delete(entity);
        }

        public clsWachtWoordGroepModel Find()
        {
            return _repo.Find();
        }

        public ObservableCollection<clsWachtWoordGroepModel> GetAll()
        {
            return _repo.GetAll();
        }

        public clsWachtWoordGroepModel GetById(int id)
        {
            return _repo.GetById(id);
        }

        public clsWachtWoordGroepModel GetFirst()
        {
            return _repo.GetFirst();
        }

        public bool Insert(clsWachtWoordGroepModel entity)
        {
            return _repo.Insert(entity);
        }

        public bool Update(clsWachtWoordGroepModel entity)
        {
            return _repo.Update(entity);
        }
    }
}
