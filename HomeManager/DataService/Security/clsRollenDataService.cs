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
    public class clsRollenDataService : IRollenDataService
    {
        IRollenRepository _repo = new clsRollenRepository();

        public bool Delete(clsRollenModel entity)
        {
            return _repo.Delete(entity);
        }

        public clsRollenModel Find()
        {
            return _repo.Find();
        }

        public ObservableCollection<clsRollenModel> GetAll()
        {
            return _repo.GetAll();
        }

        public clsRollenModel GetById(int id)
        {
            return _repo.GetById(id);
        }

        public clsRollenModel GetFirst()
        {
            return _repo.GetFirst();
        }

        public bool Insert(clsRollenModel entity)
        {
            return _repo.Insert(entity);
        }

        public bool Update(clsRollenModel entity)
        {
            return _repo.Update(entity);
        }
    }
}
