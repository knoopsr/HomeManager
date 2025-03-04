using HomeManager.Common;
using System.ComponentModel;

namespace HomeManager.Model.Personen
{
    public class clsPersoonModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {
        private int _persoonID;
        public int PersoonID
        {
            get
            {
                return _persoonID;
            }
            set
            {
                _persoonID = value;
                OnPropertyChanged();
            }
        }

        private string _Naam;
        public string Naam
        {
            get
            {
                return _Naam;
            }
            set
            {
                if (_Naam != value)
                {
                    if (_Naam != null)
                    {
                        IsDirty = true;
                    }
                }
                _Naam = value;
                OnPropertyChanged();
            }
        }

        private string _voornaam;

        public string Voornaam
        {
            get
            {
                return _voornaam;
            }
            set
            {
                if (_voornaam != value)
                {
                    if (_voornaam != null)
                    {
                        IsDirty = true;
                    }
                }
                _voornaam = value;
                OnPropertyChanged();
            }
        }

        private byte[] _foto;

        public byte[] Foto
        {
            get
            {
                return _foto;
            }
            set
            {                  
                
                _foto = value;
                OnPropertyChanged();
            }
        }

        private DateOnly _geboorteDatum;

        public DateOnly Geboortedatum
        {
            get
            {
                return _geboorteDatum;
            }
            set
            {
                if (_geboorteDatum != DateOnly.MinValue)
                {
                    if (_geboorteDatum != value)
                    {
                        IsDirty = true;
                    }
                }

                _geboorteDatum = value;
                OnPropertyChanged();
            }
        }

        private bool? _isApplicationUser;

        public bool? IsApplicationUser
        {
            get
            {
                return _isApplicationUser;
            }
            set
            {
         
                if(_isApplicationUser != null)
                {
                    if(_isApplicationUser != value)
                    {
                        IsDirty = true;
                    }
                }

                _isApplicationUser = value;
                OnPropertyChanged();       

            }
        }

        public override string ToString()
        {
            return Naam + ", " + Voornaam;
        }

        
        public string PersoonDisplayName
        {
            get
            {
                return $"{Naam} ({Voornaam})";
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(Naam):
                        if (string.IsNullOrWhiteSpace(Naam))
                        {
                            error = "Naam is een verplicht veld.";
                            if (ErrorList.Contains("Naam") == false)
                            {
                                ErrorList.Add("Naam");
                            }
                        }
                        else if (Naam.Length > 50)
                        {
                            error = "Your text is to long!!!";
                            if (ErrorList.Contains("Naam") == false)
                            {
                                ErrorList.Add("Naam");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("Naam"))
                            {
                                ErrorList.Remove("Naam");
                            }
                        }
                        return error;

                    case nameof(Voornaam):
                        if (string.IsNullOrWhiteSpace(Voornaam))
                        {
                            error = "Voornaam is een verplicht veld.";
                            if (ErrorList.Contains("Voornaam") == false)
                            {
                                ErrorList.Add("Voornaam");
                            }
                        }
                        else if (Voornaam.Length > 50)
                        {
                            error = "Voornaam text is to long!!!";
                            if (ErrorList.Contains("Voornaam") == false)
                            {
                                ErrorList.Add("Voornaam");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("Voornaam"))
                            {
                                ErrorList.Remove("Voornaam");
                            }
                        }
                        return error;

                    case nameof(IsApplicationUser):
                        if (IsApplicationUser == null)
                        {
                            error = "IsApplicationUser is een verplicht veld.";
                            if (ErrorList.Contains(nameof(IsApplicationUser)) == false)
                            {
                                ErrorList.Add(nameof(IsApplicationUser));
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains(nameof(IsApplicationUser)))
                            {
                                ErrorList.Remove(nameof(IsApplicationUser));
                            }
                        }
                        return error;



                    default:
                        error = null;
                        return error;
                }
            }
        }

    }
}
