using HomeManager.DAL.Exceptions;
using HomeManager.DAL.Logging;
using HomeManager.Model.Exceptions;
using HomeManager.Model.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DataService.Exceptions
{
    public class clsExceptionsDataService : IExceptionsDataService
    {
        IExceptionsRepsitory Repo = new clsExceptionsRepository();


        public bool Delete(clsExceptionsModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsExceptionsModel Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsExceptionsModel> GetAll()
        {
            return Repo.GetAll();
        }

        public ObservableCollection<clsExceptionsModel> GetAllByAccountID(int accountID)
        {
            return Repo.GetAllByAccountID(accountID);
        }

        public ObservableCollection<clsExceptionsModel> GetAllByExceptionName(string exceptionName)
        {
            return Repo.GetAllByExceptionName(exceptionName);
        }

        public ObservableCollection<clsExceptionsModel> GetAllByTargetSite(string targetSite)
        {
            return Repo.GetAllByTargetSite(targetSite);
        }

        public ObservableCollection<clsExceptionsModel> GetAllBydate(DateTime startDate, DateTime endDate)
        {
            return Repo.GetAllBydate(startDate, endDate);
        }

        public clsExceptionsModel GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsExceptionsModel GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert(clsExceptionsModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsExceptionsModel entity)
        {
            return Repo.Update(entity);
        }
    }
}
