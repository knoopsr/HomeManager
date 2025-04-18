using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Homepage
{
    public class clsFavorieteApplicatieModel : clsCommonModelPropertiesBase
    {
        private int _applicationID;
        public int ApplicationID
        {
            get => _applicationID;
            set
            {
                _applicationID = value;
                OnPropertyChanged();
            }
        }

        private int _accountID;
        public int AccountID
        {
            get => _accountID;
            set
            {
                _accountID = value;
                OnPropertyChanged();
            }
        }

        private string _applicationName;
        public string ApplicationName
        {
            get => _applicationName;
            set
            {
                _applicationName = value;
                OnPropertyChanged();
            }
        }

        private string _applicationPath;
        public string ApplicationPath
        {
            get => _applicationPath;
            set
            {
                _applicationPath = value;
                OnPropertyChanged();
            }
        }

        private string _iconPath;
        public string IconPath
        {
            get => _iconPath;
            set
            {
                _iconPath = value;
                OnPropertyChanged();
            }
        }
    }
}
