using HomeManager.Common;

namespace HomeManager.Model.Security
{
    /// <summary>
    /// Singleton-model dat informatie bevat over de huidige ingelogde gebruiker.
    /// </summary>
    public class clsLoginModel : clsCommonModelPropertiesBase
    {
        #region Singleton

        private static clsLoginModel? _instance;

        /// <summary>
        /// Private constructor om directe instantiatie te voorkomen.
        /// </summary>
        private clsLoginModel() { }

        /// <summary>
        /// Enige instantie van het Login-model (singleton pattern).
        /// </summary>
        public static clsLoginModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new clsLoginModel();
                return _instance;
            }
        }

        #endregion

        #region Properties - Accountgegevens

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

        private int _persoonID;
        public int PersoonID
        {
            get => _persoonID;
            set
            {
                _persoonID = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Properties - Naam en Rol

        private string _naam;
        public string Naam
        {
            get => _naam;
            set
            {
                _naam = value;
                OnPropertyChanged();
            }
        }

        private string _voorNaam;
        public string VoorNaam
        {
            get => _voorNaam;
            set
            {
                _voorNaam = value;
                OnPropertyChanged();
            }
        }

        private int _rolID;
        public int RolID
        {
            get => _rolID;
            set
            {
                _rolID = value;
                OnPropertyChanged();
            }
        }

        private string _rolName;
        public string RolName
        {
            get => _rolName;
            set
            {
                _rolName = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Properties - Status en rechten

        private int _countFailLogins;
        public int CountFailLogins
        {
            get => _countFailLogins;
            set
            {
                _countFailLogins = value;
                OnPropertyChanged();
            }
        }

        private bool _isNew;
        public bool IsNew
        {
            get => _isNew;
            set
            {
                _isNew = value;
                OnPropertyChanged();
            }
        }

        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set
            {
                _isLock = value;
                OnPropertyChanged();
            }
        }

        private string _rechtenCodes;
        public string RechtenCodes
        {
            get => _rechtenCodes;
            set
            {
                _rechtenCodes = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Properties - Overig

        private byte[]? _foto;
        public byte[]? Foto
        {
            get => _foto;
            set
            {
                _foto = value;
                OnPropertyChanged();
            }
        }

        private int _errorCode;
        public int ErrorCode
        {
            get => _errorCode;
            set
            {
                _errorCode = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
