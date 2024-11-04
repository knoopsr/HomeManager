﻿using HomeManager.ViewModel;
using System;
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
        public clsFunctieVM FunctieViewModel
        {
            get
            {
                return new clsFunctieVM();
            }
        }

        public clsEmailTypeVM EmailTypeViewModel
        {
            get
            {
                return new clsEmailTypeVM();
            }
        }

        public clsTelefoonTypeVM TelefoonTypeViewModel
        {
            get
            {
                return new clsTelefoonTypeVM();
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
        #endregion

        public clsCategorieViewModel CategorieViewModel
        {
            get
            {
                return new clsCategorieViewModel(); ;
            }

        }
    }
}
