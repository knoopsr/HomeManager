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
    public class clsCredentialManagementDataService : ICredentialManagementDataService
    {

        ICredentialManagementRepository _repo = new clsCredentialManagementRepository();
        public bool Delete(clsCredentialManagementModel entity)
        {
            return _repo.Delete(entity);
        }

        public clsCredentialManagementModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsCredentialManagementModel> GetAll()
        {
            return _repo.GetAll();
        }

        public clsCredentialManagementModel GetById(int id)
        {
            return _repo.GetById(id);
        }

        public clsCredentialManagementModel GetFirst()
        {
            return _repo.GetFirst();
        }

        public bool Insert(clsCredentialManagementModel entity)
        {
            return _repo.Insert(entity);
        }

        public bool Update(clsCredentialManagementModel entity)
        {
            return _repo.Update(entity);
        }
    }
}
