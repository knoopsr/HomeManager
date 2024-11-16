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

        public clsFrequentieViewModel FrequentieViewModel
        {
            get
            {
                return new clsFrequentieViewModel(); ;
            }
        }

        public clsBegunstigdenViewModel BegunstigdenViewModel
        {
            get
            {
                return new clsBegunstigdenViewModel();
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
    }
}
