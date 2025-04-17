using HomeManager.DataService.Exceptions;
using HomeManager.Model.Exceptions;
using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Services
{
    public static class clsExceptionService
    {
        private static readonly clsExceptionsDataService ExceptionsDataService = new clsExceptionsDataService();

        public static void InsertException(Exception ex)
        {
            ExceptionsDataService.Insert(new clsExceptionsModel()
            {
                AccountID = clsLoginModel.Instance.AccountID,
                ExceptionName = ex.GetType().FullName ?? "Unknown GetType().FullName",
                Module = ex.GetType().Module.Name ?? "Unknown Module.Name",
                Source = ex.Source ?? "Unknown Source",
                TargetSite = ex.TargetSite?.Name ?? "Unknown TargetSite",
                ExceptionMessage = ex.Message ?? "Unknown Message",
                InnerExceptionMessage = ex.InnerException?.Message ?? "Unknown InnerException.Message",
                StackTrace = ex.StackTrace ?? "Unknown StackTrace",
                DotNetAssembly = ex.GetType().Assembly.FullName ?? "Unknown Assembly"
            });
        }
    }
}
