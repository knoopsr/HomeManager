using System;
using System.ComponentModel;
using HomeManager.Common;
using HomeManager.Model.Personen;

namespace HomeManager.Model.Todo
{
    public class clsTodoPopupM : clsCommonModelPropertiesBase, IDataErrorInfo
    {
        private static clsTodoPopupM? _instance;

        public static clsTodoPopupM Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new clsTodoPopupM();
                }
                return _instance;
            }
        }
        public int PersoonID { get; set; }

        private int _todoID;
        public int TodoID
        {
            get => _todoID;
            set
            {
                _todoID = value;
                OnPropertyChanged();
            }
        }

        private string _onderwerp;
        public string Onderwerp
        {
            get => _onderwerp;
            set
            {
                if (_onderwerp != value)
                {
                    if (_onderwerp != null)
                    {
                        IsDirty = true;
                    }
                    _onderwerp = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _detail;
        public string Detail
        {
            get => _detail;
            set
            {
                if (_detail != value)
                {
                    if (_detail != null)
                    {
                        IsDirty = true;
                    }
                    _detail = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _gebruikerID;
        public int GebruikerID
        {
            get => _gebruikerID;
            set
            {
                _gebruikerID = value;
                OnPropertyChanged();
            }
        }

        private bool _belangrijk;
        public bool Belangrijk
        {
            get { return _belangrijk; }
            set
            {
                if (_belangrijk != value)
                {
                    _belangrijk = value;
                    OnPropertyChanged(nameof(Belangrijk));
                }
            }
        }

        private int? _todoCollectieID;
        public int? TodoCollectieID
        {
            get => _todoCollectieID;
            set
            {
                _todoCollectieID = value;
                OnPropertyChanged();
            }
        }

        private int? _todoCategorieID;
        public int? TodoCategorieID
        {
            get => _todoCategorieID;
            set
            {
                _todoCategorieID = value;
                OnPropertyChanged();
            }
        }

        private int? _todoColorID;
        public int? TodoColorID
        {
            get => _todoColorID;
            set
            {
                _todoColorID = value;
                OnPropertyChanged();
            }
        }

        private bool _isKlaar;
        public bool IsKlaar
        {
            get => _isKlaar;
            set
            {
                _isKlaar = value;
                OnPropertyChanged();
            }
        }

        private int? _volgorde;
        public int? Volgorde
        {
            get => _volgorde;
            set
            {
                _volgorde = value;
                OnPropertyChanged();
            }
        }
       
        public override string ToString()
        {
            return Onderwerp;
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(Onderwerp):
                        if (string.IsNullOrWhiteSpace(Onderwerp))
                        {
                            error = "Onderwerp is een verplicht veld.";
                            if (!ErrorList.Contains(nameof(Onderwerp)))
                            {
                                ErrorList.Add(nameof(Onderwerp));
                            }
                        }
                        else if (Onderwerp.Length > 100)
                        {
                            error = "Onderwerp mag maximaal 100 tekens lang zijn.";
                            if (!ErrorList.Contains(nameof(Onderwerp)))
                            {
                                ErrorList.Add(nameof(Onderwerp));
                            }
                        }
                        else
                        {
                            ErrorList.Remove(nameof(Onderwerp));
                        }
                        break;

                    case nameof(Detail):
                        if (Detail?.Length > 1000)
                        {
                            error = "Detail mag maximaal 1000 tekens lang zijn.";
                            if (!ErrorList.Contains(nameof(Detail)))
                            {
                                ErrorList.Add(nameof(Detail));
                            }
                        }
                        else
                        {
                            ErrorList.Remove(nameof(Detail));
                        }
                        break;

                    case nameof(GebruikerID):
                        if (GebruikerID <= 0)
                        {
                            error = "Een gebruiker moet geselecteerd worden.";
                            if (!ErrorList.Contains(nameof(GebruikerID)))
                            {
                                ErrorList.Add(nameof(GebruikerID));
                            }
                        }
                        else
                        {
                            ErrorList.Remove(nameof(GebruikerID));
                        }
                        break;

                    case nameof(TodoCategorieID):
                        if (TodoCategorieID == null || TodoCategorieID <= 0)
                        {
                            error = "Categorie is een verplicht veld.";
                            if (!ErrorList.Contains(nameof(TodoCategorieID)))
                            {
                                ErrorList.Add(nameof(TodoCategorieID));
                            }
                        }
                        else
                        {
                            ErrorList.Remove(nameof(TodoCategorieID));
                        }
                        break;

                    case nameof(TodoColorID):
                        if (TodoColorID == null || TodoColorID <= 0)
                        {
                            error = "Color is een verplicht veld.";
                            if (!ErrorList.Contains(nameof(TodoColorID)))
                            {
                                ErrorList.Add(nameof(TodoColorID));
                            }
                        }
                        else
                        {
                            ErrorList.Remove(nameof(TodoColorID));
                        }
                        break;

                    default:
                        break;
                }
                return error;
            }
        }
        public string Error => null;

        public static implicit operator clsTodoPopupM(clsEmailAdressenModel v)
        {
            throw new NotImplementedException();
        }
    }
}
