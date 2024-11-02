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
    public class clsRechtenDataService : IRechtenDataService
    {
        IRechtenRepository _repo = new clsRechtenRepository();

        public bool Delete(clsRechtenModel entity)
        {
            return _repo.Delete(entity);
        }

        public clsRechtenModel Find()
        {
            return _repo.Find();
        }

        public ObservableCollection<clsRechtenModel> GetAll()
        {
            return _repo.GetAll();
        }

        public clsRechtenModel GetById(int id)
        {
            return _repo.GetById(id);
        }

        public clsRechtenModel GetByCatogorieID(int id)
        {
            return _repo.GetByCatogorieID(id);
        }



        public clsRechtenModel GetFirst()
        {
            return _repo.GetFirst();
        }

        public bool Insert(clsRechtenModel entity)
        {
            return _repo.Insert(entity);
        }

        public bool Update(clsRechtenModel entity)
        {
            return _repo.Update(entity);
        }


    }
}
