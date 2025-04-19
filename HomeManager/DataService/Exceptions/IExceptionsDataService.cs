using HomeManager.Common;
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
    interface IExceptionsDataService : IDataService<clsExceptionsModel>
    {
        ObservableCollection<clsExceptionsModel> GetAllByAccountID(int accountID);
        ObservableCollection<clsExceptionsModel> GetAllByExceptionName(string exceptionName);
        ObservableCollection<clsExceptionsModel> GetAllByTargetSite(string targetSite);
        ObservableCollection<clsExceptionsModel> GetAllBydate(DateTime startDate, DateTime endDate);
    }
}
