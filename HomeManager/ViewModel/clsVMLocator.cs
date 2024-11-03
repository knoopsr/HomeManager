﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.ViewModel
{
    public class clsVMLocator
    {

        public clsPersoonVM PersoonViewModel
        {
            get
            {
                return new clsPersoonVM();
            }
        }

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

        public clsAccountViewModel AccountViewModel
        {
            get
            {
                return new clsAccountViewModel();
            }
        }

        public clsCredentialManagementViewModel CredentialManagementViewModel
        {
            get
            {
                return new clsCredentialManagementViewModel();
            }
        }
        public clsLogin LoginViewModel
        {
            get
            {
                return new clsLogin();
            }
        }
        public clsNewPassViewModel NewPassViewModel
        {
            get
            {
                return new clsNewPassViewModel();
            }
        }

        #endregion
    }
}
