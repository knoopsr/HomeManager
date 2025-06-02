using HomeManager.Common;
using System.ComponentModel;

namespace HomeManager.Model.Security
{
    /// <summary>
    /// Representatie van een account met login-informatie en validatie.
    /// </summary>
    public class clsAccountModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {
        #region Overrides

        public override string ToString() => Login;

        #endregion

        #region IDataErrorInfo implementatie

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(Login):
                        if (string.IsNullOrWhiteSpace(Login))
                        {
                            error = "Login is verplicht veld.";
                            if (!ErrorList.Contains(nameof(Login)))
                                ErrorList.Add(nameof(Login));
                        }
                        else if (Login.Length > 50)
                        {
                            error = "Login mag niet langer zijn dan 50 karakters.";
                            if (!ErrorList.Contains(nameof(Login)))
                                ErrorList.Add(nameof(Login));
                        }
                        else
                        {
                            ErrorList.Remove(nameof(Login));
                        }
                        return error;

                    case nameof(Wachtwoord):
                        if (Wachtwoord?.Length > 50)
                        {
                            error = "Wachtwoord mag niet langer zijn dan 50 karakters.";
                            if (!ErrorList.Contains(nameof(Wachtwoord)))
                                ErrorList.Add(nameof(Wachtwoord));
                        }
                        else
                        {
                            ErrorList.Remove(nameof(Wachtwoord));
                        }
                        return error;

                    default:
                        return null;
                }
            }
        }

        public string Error => null;

        #endregion

        #region Properties

        /// <summary>
        /// Unieke ID van het account.
        /// </summary>
        public int AccountID { get; set; }

        private int _persoonID;
        /// <summary>
        /// Koppeling naar de persoon.
        /// </summary>
        public int PersoonID
        {
            get => _persoonID;
            set
            {
                if (_persoonID != value && _persoonID != 0)
                    IsDirty = true;
                _persoonID = value;
                OnPropertyChanged();
            }
        }

        private int _rolID;
        /// <summary>
        /// ID van de toegewezen rol.
        /// </summary>
        public int RolID
        {
            get => _rolID;
            set
            {
                if (_rolID != value && _rolID != 0)
                    IsDirty = true;
                _rolID = value;
                OnPropertyChanged();
            }
        }

        private string _wachtwoord;
        /// <summary>
        /// Het wachtwoord van de gebruiker (versleuteld of in plain tekst afhankelijk van context).
        /// </summary>
        public string Wachtwoord
        {
            get => _wachtwoord;
            set
            {
                if (_wachtwoord != value && _wachtwoord != null)
                    IsDirty = true;
                _wachtwoord = value;
                OnPropertyChanged();
            }
        }

        private string _login;
        /// <summary>
        /// Gebruikersnaam (login).
        /// </summary>
        public string Login
        {
            get => _login;
            set
            {
                if (_login != value && _login != null)
                    IsDirty = true;
                _login = value;
                OnPropertyChanged();
            }
        }

        private string _accountName;
        /// <summary>
        /// Volledige accountnaam of alias (optioneel).
        /// </summary>
        public string AccountName
        {
            get => _accountName;
            set
            {
                if (_accountName != value && _accountName != null)
                    IsDirty = true;
                _accountName = value;
                OnPropertyChanged();
            }
        }

        private bool _isNew;
        /// <summary>
        /// Indicatie of dit een nieuw account is.
        /// </summary>
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
        /// <summary>
        /// Indicatie of het account vergrendeld is.
        /// </summary>
        public bool IsLock
        {
            get => _isLock;
            set
            {
                _isLock = value;
                OnPropertyChanged();
            }
        }

        private int _countFailLogins;
        /// <summary>
        /// Aantal mislukte loginpogingen.
        /// </summary>
        public int CountFailLogins
        {
            get => _countFailLogins;
            set
            {
                _countFailLogins = value;
                OnPropertyChanged();
            }
        }

        private string _rolNaam;
        /// <summary>
        /// De naam van de rol waaraan dit account is gekoppeld.
        /// </summary>
        public string RolNaam
        {
            get => _rolNaam;
            set
            {
                _rolNaam = value;
                OnPropertyChanged();
            }
        }

        private byte[] _foto;
        /// <summary>
        /// Profielfoto of afbeelding gekoppeld aan het account.
        /// </summary>
        public byte[] Foto
        {
            get => _foto;
            set
            {
                _foto = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
