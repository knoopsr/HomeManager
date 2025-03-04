using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Common;

namespace HomeManager.Model.Logging
{
    public class clsButtonLoggingModel
    {
		public int ButtonLogId { get; set; } //pk

		public int AccountId { get; set; } //FK waarmee ik Logging.tblButtonLogs join met Security.tblAccount

		public string AccountName { get; set; } //dit is Security.tblAccount.Login

		public string ActionName { get; set; }

		public string ActionTarget { get; set; }

		public DateTime LogTime { get; set; } //createdOn in db


	}
}
