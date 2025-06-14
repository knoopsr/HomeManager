using HomeManager.ViewModel.Personen;
using HomeManager.ViewModel.Homepage;
using HomeManager.ViewModel.Logging;
using HomeManager.ViewModel.Security;
using HomeManager.ViewModel.Todo;
using HomeManager.ViewModel.Exceptions;
using HomeManager.ViewModel.StickyNotes;

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

        private static clsEmailVerzendenViewModel _EmailVerzenden = new clsEmailVerzendenViewModel();
        public clsEmailVerzendenViewModel EmailVerzendenViewModel
        {
            get
            {
                return _EmailVerzenden;

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

        #region Homepage
        public clsFavorieteApplicatieViewModel FavorieteApplicatieViewModel
        {
            get
            {
                return new clsFavorieteApplicatieViewModel();
            }
        }
        public clsSnelkoppelingViewModel SnelkoppelingViewModel
        {
            get
            {
                return new clsSnelkoppelingViewModel();
            }
        }
        public clsFavorieteVensterViewModel FavorieteVensterViewModel
        {
            get
            {
                return new clsFavorieteVensterViewModel();
            }
        }
        public clsFotoCarouselViewModel FotoCarouselViewModel
        {
            get
            {
                return new clsFotoCarouselViewModel();
            }
        }
        public clsWeerViewModel WeerViewModel
        {
            get
            {
                return new clsWeerViewModel();
            }
        }
        public clsProfielViewModel ProfielViewModel
        {
            get
            {
                return new clsProfielViewModel();
            }
        }


        #endregion

        #region Todo

        private static readonly clsCollectiesVM _collectiesViewModel = new clsCollectiesVM();
        public clsCollectiesVM CollectiesViewModel
        {
            get
            {
                return _collectiesViewModel;
            }
        }

        public clsCategorieënVM CategorieënViewModel => new clsCategorieënVM();
        public clsKleurenVM KleurenViewModel => new clsKleurenVM();
        public clsTodoVM ToDoViewModel
        {
            get
            {
                return new clsTodoVM();
            }
        }


        private static readonly clsTodoPopupVM _todoPopupViewModel = new clsTodoPopupVM();
        public clsTodoPopupVM TodoPopupViewModel
        {
            get
            {
                return _todoPopupViewModel;
            }
        }

        private static readonly clsTodoDetailsVM _todoDetailsViewModel = new clsTodoDetailsVM(0);
        public clsTodoDetailsVM TodoDetailsViewModel
        {
            get
            {
                return _todoDetailsViewModel;
            }
        }

        public clsTodoBijlageVM TodoBijlageViewModel
        {
            get
            {
                return new clsTodoBijlageVM();
            }
        }

        #endregion

        #region EXCEPTIONS + LOGGING
        public clsExceptionsViewModel ExceptionsViewModel { get => new clsExceptionsViewModel(); }
        public clsExceptionsMailViewModel ExceptionsMailViewModel { get => new clsExceptionsMailViewModel(); }

        public clsButtonLoggingViewModel ButtonLoggingViewModel
        {
            get
            {
                return new clsButtonLoggingViewModel();
            }
        }
        #endregion

        #region Budget
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
        public static clsTransactieViewModel TransactieViewModel
        {
            get
            {
                return _transactieViewModel;
            }
        }

        public clsOverzichtViewModel OverzichtViewModel
        {
            get
            {
                return new clsOverzichtViewModel();
            }
        }
        #endregion

        #region STICKY NOTES
        private static readonly clsStickyNotesViewModel _stickyNotesViewModel = new clsStickyNotesViewModel();
        public clsStickyNotesViewModel StickyNotesViewModel { get => _stickyNotesViewModel; }
        #endregion
    }
}