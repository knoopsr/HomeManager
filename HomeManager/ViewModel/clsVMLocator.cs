using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.ViewModel
{
    public class clsVMLocator
    {
        #region Security
        public clsRechtenViewModel RechtenViewModel
        {
            get
            {
                return new clsRechtenViewModel();
            }
        }

        public clsCredentialGroupViewModel CredentialGroupViewModel
        {
            get
            {
                return new clsCredentialGroupViewModel();
            }
        }

        public clsAccountsViewModel AccountsViewModel
        {
            get
            {
                return new clsAccountsViewModel();
            }
        }

        public clsCredentialManagementViewModel CredentialManagementViewModel
        {
            get
            {
                return new clsCredentialManagementViewModel();
            }
        }



        #endregion
    }
}
