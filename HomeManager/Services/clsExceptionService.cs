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
    /// <summary>
    /// Service for handling exceptions. This class inserts exception details into the database
    /// and sends exception information via email to the current user and the development team.
    /// </summary>
    public static class clsExceptionService
    {
        /// <summary>
        /// Data service for inserting exception data into the database.
        /// </summary>
        private static readonly clsExceptionsDataService ExceptionsDataService = new clsExceptionsDataService();

        /// <summary>
        /// Stores the last exception to prevent duplicate logging of the same exception.
        /// </summary>
        private static clsExceptionsModel _oldException;

        /// <summary>
        /// Inserts the given exception into the database and emails the exception details
        /// to the current user and the development team.
        /// </summary>
        /// <param name="ex">The exception to be inserted into the database and emailed.</param>
        /// <remarks>
        /// The method creates a <see cref="clsExceptionsModel"/> with relevant details from the exception
        /// and ensures that duplicate exceptions are not logged repeatedly by comparing the current exception
        /// to the previous one.
        /// </remarks>
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
