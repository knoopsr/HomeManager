using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Model.Todo;
using HomeManager.ViewModel;
using HomeManager.ViewModel.Todo;

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
        private static readonly clsCollectiesVM _collectiesViewModel = new clsCollectiesVM();
        public clsCollectiesVM CollectiesViewModel
        {
            get
            {
                return _collectiesViewModel;
            }
        }

        public clsCategorieënVM CategorieënViewModel
        {
            get
            {
                return new clsCategorieënVM();
            }
        }

        public clsKleurenVM KleurenViewModel
        {
            get
            {
                return new clsKleurenVM();
            }
        }

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

        private static readonly clsTodoDetailsVM _todoDetailsViewModel = new clsTodoDetailsVM();
        public clsTodoDetailsVM TodoDetailsViewModel
        {
            get
            {
                return _todoDetailsViewModel;
            }
        }
        #endregion
    }
}
