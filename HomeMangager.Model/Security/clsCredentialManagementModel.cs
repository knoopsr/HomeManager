using HomeManager.Common;
using System.ComponentModel;

namespace HomeManager.Model.Security
{
    /// <summary>
    /// Model voor beheer van opgeslagen logins en wachtwoorden.
    /// </summary>
    public class clsCredentialManagementModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {
        #region IDataErrorInfo implementatie

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;

                switch (columnName)
                {
                    case nameof(WachtwoordNaam):
                        if (string.IsNullOrWhiteSpace(WachtwoordNaam))
                        {
                            error = "WachtwoordNaam is verplicht veld.";
                            AddError(nameof(WachtwoordNaam));
                        }
                        else if (WachtwoordNaam.Length > 50)
                        {
                            error = "WachtwoordNaam mag niet langer zijn dan 50 karakters.";
                            AddError(nameof(WachtwoordNaam));
                        }
                        else
                        {
                            RemoveError(nameof(WachtwoordNaam));
                        }
                        break;

                    case nameof(WachtwoordOmschrijving):
                        if (!string.IsNullOrEmpty(WachtwoordOmschrijving) && WachtwoordOmschrijving.Length > 1000)
                        {
                            error = "WachtwoordOmschrijving mag niet langer zijn dan 1000 karakters.";
                            AddError(nameof(WachtwoordOmschrijving));
                        }
                        else
                        {
                            RemoveError(nameof(WachtwoordOmschrijving));
                        }
                        break;

                    case nameof(Login):
                        if (string.IsNullOrWhiteSpace(Login))
                        {
                            error = "Login is verplicht veld.";
                            AddError(nameof(Login));
                        }
                        else if (Login.Length > 50)
                        {
                            error = "Login mag niet langer zijn dan 50 karakters.";
                            AddError(nameof(Login));
                        }
                        else
                        {
                            RemoveError(nameof(Login));
                        }
                        break;

                    case nameof(Wachtwoord):
                        if (string.IsNullOrWhiteSpace(Wachtwoord))
                        {
                            error = "Wachtwoord is verplicht veld.";
                            AddError(nameof(Wachtwoord));
                        }
                        else if (Wachtwoord.Length > 50)
                        {
                            error = "Wachtwoord mag niet langer zijn dan 50 karakters.";
                            AddError(nameof(Wachtwoord));
                        }
                        else
                        {
                            RemoveError(nameof(Wachtwoord));
                        }
                        break;
                }

                return error;
            }
        }

        public string Error => null;

        private void AddError(string name)
        {
            if (!ErrorList.Contains(name)) ErrorList.Add(name);
        }

        private void RemoveError(string name)
        {
            if (ErrorList.Contains(name)) ErrorList.Remove(name);
        }

        #endregion

        #region Properties

        private int _wachtwoordID;
        /// <summary>
        /// Unieke ID van het wachtwoordrecord.
        /// </summary>
        public int WachtwoordID
        {
            get => _wachtwoordID;
            set
            {
                _wachtwoordID = value;
                OnPropertyChanged();
            }
        }

        private int _wachtwoordGroepID;
        /// <summary>
        /// ID van de groep waartoe het wachtwoord behoort.
        /// </summary>
        public int WachtwoordGroepID
        {
            get => _wachtwoordGroepID;
            set
            {
                if (_wachtwoordGroepID != value && _wachtwoordGroepID != 0)
                    IsDirty = true;
                _wachtwoordGroepID = value;
                OnPropertyChanged();
            }
        }

        private string _wachtwoordGroepNaam;
        /// <summary>
        /// Naam van de wachtwoordgroep.
        /// </summary>
        public string WachtwoordGroepNaam
        {
            get => _wachtwoordGroepNaam;
            set
            {
                _wachtwoordGroepNaam = value;
                OnPropertyChanged();
            }
        }

        private string _wachtwoordNaam;
        /// <summary>
        /// Naam/titel van de login.
        /// </summary>
        public string WachtwoordNaam
        {
            get => _wachtwoordNaam;
            set
            {
                if (_wachtwoordNaam != value && _wachtwoordNaam != null)
                    IsDirty = true;
                _wachtwoordNaam = value;
                OnPropertyChanged();
            }
        }

        private string _wachtwoordOmschrijving;
        /// <summary>
        /// Beschrijving of toelichting bij de login.
        /// </summary>
        public string WachtwoordOmschrijving
        {
            get => _wachtwoordOmschrijving;
            set
            {
                if (_wachtwoordOmschrijving != value && _wachtwoordOmschrijving != null)
                    IsDirty = true;
                _wachtwoordOmschrijving = value;
                OnPropertyChanged();
            }
        }

        private string _login;
        /// <summary>
        /// Gebruikersnaam van de login.
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

        private string _wachtwoord;
        /// <summary>
        /// Het wachtwoord van de login.
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

        #endregion
    }
}
