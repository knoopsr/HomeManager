using HomeManager.Common;
using HomeManager.Model.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.Logging
{
    public interface IButtonLoggingRepsitory : IRepository<clsButtonLoggingModel>
    {
        ObservableCollection<clsButtonLoggingModel> GetAllByAccountId(int accountId);
        ObservableCollection<clsButtonLoggingModel> GetAllByActionName(string actionName);
        ObservableCollection<clsButtonLoggingModel> GetAllByActionTarget(string actionTarget);
        ObservableCollection<clsButtonLoggingModel> GetAllBydate(DateTime startDate, DateTime endDate);
    }
}
