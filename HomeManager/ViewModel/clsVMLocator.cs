using HomeManager.Model.Personen;
using HomeManager.ViewModel;
using HomeManager.ViewModel.Logging;
using HomeManager.ViewModel.Security;
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
        #region Personen
        private static clsPersoonViewModel _persoonViewModel = new clsPersoonViewModel();

        public clsPersoonViewModel PersoonViewModel
        {
            get
            {
                return _persoonViewModel;
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
        private static clsEmailAdressenViewModel _EmailAdressen = new clsEmailAdressenViewModel();
        public clsEmailAdressenViewModel EmailAdressenViewModel
        {
            get
            {
                return _EmailAdressen;
                //return new clsEmailAdressenViewModel();
            }
        }

        public clsPersonenViewModel PersonenViewModel
        {
            get
            {
                return new clsPersonenViewModel();
            }
        }

        private static clsAdressenViewModel _Adressen = new clsAdressenViewModel();

        public clsAdressenViewModel AdressenViewModel
        {
            get
            {
                return _Adressen;
                //return new clsAdressenViewModel();
            }
        }


        private static clsTelefoonNummersViewModel _TelefoonNummers = new clsTelefoonNummersViewModel();
        public clsTelefoonNummersViewModel TelefoonNummersViewModel
        {
            get
            {
                return _TelefoonNummers;
                //return new clsTelefoonNummersViewModel();
            }
        }

        private static clsNotitiesViewModel _Notities = new clsNotitiesViewModel();
        public clsNotitiesViewModel NotitiesViewModel
        {
            get
            {
                return _Notities;
                //return new clsNotitiesViewModel();
            }
        }

#endregion

        #region DagBoek
        //private static clsDagboekViewModel _dagboekViewModel = new clsDagboekViewModel();
        public clsDagboekViewModel DagboekViewModel
        {
            get
            {
                //return _dagboekViewModel;
                return new clsDagboekViewModel();
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

       
        public clsUnLockViewModel UnLockViewModel
        {
            get
            {
                return new clsUnLockViewModel();
            }
        }


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
