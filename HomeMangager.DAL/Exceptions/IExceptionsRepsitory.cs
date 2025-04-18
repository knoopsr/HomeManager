using HomeManager.Common;
using HomeManager.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.Exceptions
{
    public interface IExceptionsRepsitory : IRepository<clsExceptionsModel>
    {
        ObservableCollection<clsExceptionsModel> GetAllByAccountID(int accountID);
        ObservableCollection<clsExceptionsModel> GetAllByExceptionName(string exceptionName);
        ObservableCollection<clsExceptionsModel> GetAllByTargetSite(string targetSite);
        ObservableCollection<clsExceptionsModel> GetAllBydate(DateTime startDate, DateTime endDate);
    }
}
