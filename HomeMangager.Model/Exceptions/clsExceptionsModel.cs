using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Exceptions
{
    public class clsExceptionsModel
    {
        #region PROPERTIES
        // Basic info
        public int ExceptionID { get; set; }
        public int AccountID { get; set; }
        public string AccountName { get; set; }
        public string ExceptionName { get; set; }
        // Detailed info
        public string Module { get; set; }
        public string Source { get; set; }
        public string TargetSite { get; set; }
        public string ExceptionMessage { get; set; }
        public string InnerExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public string DotNetAssembly { get; set; }
        // Date
        public DateTime CreatedOn { get; set; }
        #endregion
    }
}
