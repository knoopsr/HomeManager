using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Common;


namespace HomeManager.Model.Security
{
    public class clsLoginModel : clsCommonModelPropertiesBase
    {



        // Singleton instantie
        private static clsLoginModel? _instance;

        // Private constructor om te voorkomen dat er andere instanties worden aangemaakt
        private clsLoginModel() { }


        // Publieke eigenschap om de enkele instantie op te halen
        public static clsLoginModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new clsLoginModel();
                }
                return _instance;
            }
        }



        private int _accountID;
        public int AccountID
        {
            get { return _accountID; }
            set
            {
                _accountID = value;
                OnPropertyChanged();
            }
        }

        private int _persoonID;
        public int PersoonID
        {
            get { return _persoonID; }
            set
            {           
                _persoonID = value;
                OnPropertyChanged();
            }
        }

        private string _naam;
        public string Naam
        {
            get { return _naam; }
            set
            {
                _naam = value;
                OnPropertyChanged();
            }
        }

        private string _voorNaam;
        public string VoorNaam
        {
            get { return _voorNaam; }
            set
            {
                _voorNaam = value;
                OnPropertyChanged();
            }
        }
        private int _rolID;
        public int RolID
        {
            get { return _rolID; }
            set
            {
                _rolID = value;
                OnPropertyChanged();
            }
        }

        private string _rolName;
        public string RolName
        {
            get { return _rolName; }
            set
            {
                _rolName = value;
                OnPropertyChanged();
            }
        }
        private int _countFailLogins;
        public int CountFailLogins
        {
            get { return _countFailLogins; }
            set
            {
                _countFailLogins = value;
                OnPropertyChanged();
            }
        }

        private bool _isNew;
        public bool IsNew
        {
            get { return _isNew; }
            set
            {
                _isNew = value;
                OnPropertyChanged();
            }
        }
        private bool _isLock;
        public bool IsLock
        {
            get { return _isLock; }
            set
            {
                _isLock = value;
                OnPropertyChanged();
            }
        }
        private string _rechtenCodes;
        public string RechtenCodes
        {
            get { return _rechtenCodes; }
            set
            {
                _rechtenCodes = value;
                OnPropertyChanged();
            }
        }

        private byte[]? _foto;
        public byte[]? Foto
        {
            get { return _foto; }
            set
            {
                _foto = value;
                OnPropertyChanged();
            }
        }

        private int _errorCode;
        public int ErrorCode
        {
            get { return _errorCode; }
            set
            {
                _errorCode = value;
                OnPropertyChanged();
            }
        }
    }
}
