using HomeManager.DAL.Logging;
using HomeManager.Model.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DataService.Logging
{
    public class clsButtonLoggingDataService : IButtonLoggingDataService
    {
        IButtonLoggingRepsitory Repo = new clsButtonLoggingRepository();


        public bool Delete(clsButtonLoggingModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsButtonLoggingModel Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsButtonLoggingModel> GetAll()
        {
            return Repo.GetAll();
        }

        public ObservableCollection<clsButtonLoggingModel> GetAllByAccountId(int accountId)
        {
            return Repo.GetAllByAccountId(accountId);
        }

        public ObservableCollection<clsButtonLoggingModel> GetAllByActionName(string actionName)
        {
            return Repo.GetAllByActionName(actionName);
        }

        public ObservableCollection<clsButtonLoggingModel> GetAllByActionTarget(string actionTarget)
        {
            return Repo.GetAllByActionTarget(actionTarget);
        }

        public ObservableCollection<clsButtonLoggingModel> GetAllBydate(DateTime startDate, DateTime endDate)
        {
            return Repo.GetAllBydate(startDate, endDate);
        }

        public clsButtonLoggingModel GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsButtonLoggingModel GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert(clsButtonLoggingModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsButtonLoggingModel entity)
        {
            return Repo.Update(entity);
        }
    }
}
