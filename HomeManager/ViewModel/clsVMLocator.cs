using HomeManager.Model.Personen;
using HomeManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.ViewModel
{
    public class clsVMLocator
    {
        public clsPersoonViewModel PersoonViewModel
        {
            get
            {
                return new clsPersoonViewModel();
            }
        }
        public clsFunctieViewModel FunctieViewModel
        {
            get
            {
                return new clsFunctieViewModel();
            }
        }

        public clsEmailTypeViewModel EmailTypeViewModel
        {
            get
            {
                return new clsEmailTypeViewModel();
            }
        }

        public clsTelefoonTypeViewModel TelefoonTypeViewModel
        {
            get
            {
                return new clsTelefoonTypeViewModel();
            }
        }

        public clsLandViewModel LandViewModel
        {
            get
            {
                return new clsLandViewModel();
            }
        }

        public clsProvincieViewModel ProvincieViewModel
        {
            get
            {
                return new clsProvincieViewModel();
            }
        }

        public clsGemeenteViewModel GemeenteViewModel
        {
            get
            {
                return new clsGemeenteViewModel();
            }
        }
        public clsEmailAdressenViewModel EmailAdressenViewModel
        {
            get
            {
                return new clsEmailAdressenViewModel();
            }
        }

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

        public clsCategorieViewModel CategorieViewModel
        {
            get
            {
                return new clsCategorieViewModel(); ;
            }

        }
    }
}
