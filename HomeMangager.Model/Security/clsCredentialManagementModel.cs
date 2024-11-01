using HomeManager.Common;
using System.ComponentModel;

namespace HomeManager.Model.Security
{
    public class clsCredentialManagementModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {
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
                            if (ErrorList.Contains(nameof(WachtwoordNaam)) == false)
                            {
                                ErrorList.Add(nameof(WachtwoordNaam));
                            }
                        }
                        else if (WachtwoordNaam.Length > 50)
                        {
                            error = "WachtwoordNaam mag niet langer zijn dan 50 karakters.";
                            if (ErrorList.Contains(nameof(WachtwoordNaam)) == false)
                            {
                                ErrorList.Add(nameof(WachtwoordNaam));
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains(nameof(WachtwoordNaam)))
                            {
                                ErrorList.Remove(nameof(WachtwoordNaam));
                            }
                        }
                        return error;

                    case nameof(WachtwoordOmschrijving):
                    if (WachtwoordOmschrijving.Length > 1000)
                        {
                            error = "WachtwoordOmschrijving mag niet langer zijn dan 1000 karakters.";
                            if (ErrorList.Contains(nameof(WachtwoordOmschrijving)) == false)
                            {
                                ErrorList.Add(nameof(WachtwoordOmschrijving));
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains(nameof(WachtwoordOmschrijving)))
                            {
                                ErrorList.Remove(nameof(WachtwoordOmschrijving));
                            }
                        }
                        return error;


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
                        if (string.IsNullOrWhiteSpace(Wachtwoord))
                        {
                            error = "Wachtwoord is verplicht veld.";
                            if (ErrorList.Contains(nameof(Wachtwoord)) == false)
                            {
                                ErrorList.Add(nameof(Wachtwoord));
                            }
                        }
                        else if (Wachtwoord.Length > 50)
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

        private int _wacchtwoordID;
        public int WachtwoordID
        {
            get { return _wacchtwoordID; }
            set
            {

                _wacchtwoordID = value;
                OnPropertyChanged();

            }
        }

        private int _wachtwoordGroepID;
        public int WachtwoordGroepID
        {
            get { return _wachtwoordGroepID; }
            set
            {
                if (_wachtwoordGroepID != value)
                {
                    if (_wachtwoordGroepID != 0)
                    {
                        IsDirty = true;
                    }
                }
                _wachtwoordGroepID = value;
                OnPropertyChanged();
            }
        }

        private string _wachtwoordNaam;
        public string WachtwoordNaam
        {
            get { return _wachtwoordNaam; }
            set
            {
                if (_wachtwoordNaam != value)
                {
                    if (_wachtwoordNaam != null)
                    {
                        IsDirty = true;
                    }
                }
                _wachtwoordNaam = value;
                OnPropertyChanged();
            }
        }

        private string _wachtwoordOmschrijving;
        public string WachtwoordOmschrijving
        {
            get { return _wachtwoordOmschrijving; }
            set
            {
                if (_wachtwoordOmschrijving != value)
                {
                    if (_wachtwoordOmschrijving != null)
                    {
                        IsDirty = true;
                    }
                }
                _wachtwoordOmschrijving = value;
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





    }
}
