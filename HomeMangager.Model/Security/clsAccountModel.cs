using HomeManager.Common;
using System.ComponentModel;

namespace HomeManager.Model.Security
{
    public class clsAccountModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {
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
                            if (ErrorList.Contains(nameof(Login)) == false)
                            {
                                ErrorList.Add(nameof(Login));
                            }
                        }
                        else if (Login.Length > 50)
                        {
                            error = "Login mag niet langer zijn dan 50 karakters.";
                            if (ErrorList.Contains(nameof(Login)) == false)
                            {
                                ErrorList.Add(nameof(Login));
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains(nameof(Login)))
                            {
                                ErrorList.Remove(nameof(Login));
                            }
                        }
                        return error;

                    case nameof(Wachtwoord):
                        if (Wachtwoord.Length > 50)
                        {
                            error = "Wachtwoord mag niet langer zijn dan 50 karakters.";
                            if (ErrorList.Contains(nameof(Wachtwoord)) == false)
                            {
                                ErrorList.Add(nameof(Wachtwoord));
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains(nameof(Wachtwoord)))
                            {
                                ErrorList.Remove(nameof(Wachtwoord));
                            }
                        }
                        return error;



                    default:
                        error = null;
                        return error;
                }

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
                if(_persoonID != value)
                {
                    if(_persoonID != 0)
                    {
                        IsDirty = true;
                    }
                }
                _persoonID = value;
                OnPropertyChanged();
            }
        }

        private int _rolID;
        public int RolID
        {
            get { return _rolID; }
            set
            {
                if (_rolID != value)
                {
                    if (_rolID != 0)
                    {
                        IsDirty = true;
                    }
                }
                _rolID = value;
                OnPropertyChanged();
            }
        }

        private string _wachtwoord;
        public string Wachtwoord
        {
            get { return _wachtwoord; }
            set
            {
                if (_wachtwoord != value)
                {
                    if (_wachtwoord != null)
                    {
                        IsDirty = true;
                    }
                }
                _wachtwoord = value;
                OnPropertyChanged();
            }
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                if (_login != value)
                {
                    if (_login != null)
                    {
                        IsDirty = true;
                    }
                }
                _login = value;
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


    }
}
