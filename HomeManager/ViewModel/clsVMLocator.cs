using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Model.Budget;
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

        private static clsCategorieViewModel _categorieViewModel = new clsCategorieViewModel();
        public clsCategorieViewModel CategorieViewModel
        {
            get
            {
                return _categorieViewModel; 
            }
        }

        private static clsFrequentieViewModel _frequentieViewModel = new clsFrequentieViewModel();
        public clsFrequentieViewModel FrequentieViewModel
        {
            get
            {
                return _frequentieViewModel;
            }
        }

        private static clsBegunstigdenViewModel _begunstigdenViewModel = new clsBegunstigdenViewModel();
        public clsBegunstigdenViewModel BegunstigdenViewModel
        {
            get
            {
                return _begunstigdenViewModel;
            }
        }

        private static clsDomicilieringViewModel _domicilieringViewModel = new clsDomicilieringViewModel();
        public clsDomicilieringViewModel DomicilieringViewModel
        {
            get
            {
                return _domicilieringViewModel;
            }
        }

        private static clsTransactieViewModel _transactieViewModel = new clsTransactieViewModel();
        public clsTransactieViewModel TransactieViewModel
        {
            get
            {
                return _transactieViewModel;
            }
        }
        
        private static clsOverzichtViewModel _overzichtViewModel = new clsOverzichtViewModel();
        public clsOverzichtViewModel OverzichtViewModel
        {
            get
            {
                return _overzichtViewModel;
            }
        }
        
    }
}
