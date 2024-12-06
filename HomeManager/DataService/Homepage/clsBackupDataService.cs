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
    public class clsBackupDataService : IBackupDataService
    {
        IBackupRepository _Repo = new clsBackupRepository();

        public async Task<ObservableCollection<clsBackupModel>> CreateBackup()
        {
            return await _Repo.CreateBackup();
        }

        public bool Delete(clsBackupModel entity)
        {
            return _Repo.Delete(entity);
        }

        public clsBackupModel Find()
        {
            return _Repo.Find();
        }

        public ObservableCollection<clsBackupModel> GetAll()
        {
            return _Repo.GetAll();
        }

        public clsBackupModel GetById(int id)
        {
            return _Repo.GetById(id);
        }

        public clsBackupModel GetFirst()
        {
            return _Repo.GetFirst();
        }

        public bool Insert(clsBackupModel entity)
        {
            return _Repo.Insert(entity);
        }

        public bool Update(clsBackupModel entity)
        {
            return _Repo.Update(entity);
        }
    }
}
