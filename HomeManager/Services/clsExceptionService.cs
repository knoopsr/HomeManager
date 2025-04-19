using HomeManager.DataService.Exceptions;
using HomeManager.MailService;
using HomeManager.Model.Exceptions;
using HomeManager.ViewModel;
using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.ViewModel.Exceptions;

namespace HomeManager.Services
{
    public static class clsExceptionService
    {
        private static readonly clsExceptionsDataService ExceptionsDataService = new clsExceptionsDataService();

        /// <summary>
        /// Inserts the exception into the database and also mails it to the current user & dev team.
        /// </summary>
        /// <param name="ex"></param>
        /// 
        private static clsExceptionsModel _oldException;
        public static void InsertException(Exception ex)
        {
            clsExceptionsModel exception = new clsExceptionsModel()
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
            };
            if (_oldException == exception)
            {
                return;
            }
            _oldException =  exception;           

            ExceptionsDataService.Insert(exception);
            clsExceptionsMailViewModel.SendExceptionToMailAddresses(exception);
        }
    }
}
