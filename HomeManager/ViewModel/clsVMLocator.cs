﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Model.Todo;
using HomeManager.ViewModel;

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

        #region DagBoek
        private static clsDagboekViewModel _dagboekViewModel = new clsDagboekViewModel();
        public clsDagboekViewModel DagboekViewModel
        {
            get
            {
                return _dagboekViewModel;
                //return new clsDagboekViewModel();
            }
        }

        #endregion

        #region Security
        public clsTitlePersonViewModel TitlePersonViewModel
        {
            get
            {
                return new clsTitlePersonViewModel();
            }
        }


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

        private static clsLogin _loginViewModel = new clsLogin();
        public clsLogin LoginViewModel
        {
            get
            {
                return _loginViewModel;
            }
        }

        private static clsNewPassViewModel _newPassViewModel = new clsNewPassViewModel();
        public clsNewPassViewModel NewPassViewModel
        {
            get
            {
                return _newPassViewModel;
            }
        }

        #endregion
        #region TODO
        public clsCollectiesVM CollectiesViewModel => new clsCollectiesVM();
        public clsCategorieënVM CategorieënViewModel => new clsCategorieënVM();
        public clsKleurenVM KleurenViewModel => new clsKleurenVM();
        #endregion

    }
}
