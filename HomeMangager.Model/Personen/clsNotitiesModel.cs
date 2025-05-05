using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HomeManager.Model.Personen
{
    public class clsNotitiesModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {
        private int _notitieID;
        public int NotitieID
        {
            get
            {
                return _notitieID;
            }
            set
            {
                _notitieID = value;
                OnPropertyChanged();
            }
        }
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

        private string _onderwerp;

        public string Onderwerp
        {
            get
            {
                return _onderwerp;
            }
            set
            {
                if (_onderwerp != value)
                {
                    if (_onderwerp != null)
                    {
                        IsDirty = true;
                    }
                }
                _onderwerp = value;
                OnPropertyChanged();
            }
        }

        //private string _notitie;

        //public string Notitie
        //{
        //    get
        //    {
        //        return _notitie;
        //    }
        //    set
        //    {
        //        if (_notitie != value)
        //        {
        //            if (_notitie != null)
        //            {
        //                IsDirty = true;
        //            }
        //        }
        //        _notitie = value;
        //        OnPropertyChanged();
        //    }
        //}

        private string _notitie;
        public string Notitie
        {
            get { return _notitie; }
            set
            {
                _notitie = value;
                OnPropertyChanged();
            }
        }

        private DateTime _createdOn;
        public DateTime CreatedOn
        {
            get { return _createdOn; }
            set
            {
                if (_createdOn != value)
                {
                    _createdOn = value;
                    OnPropertyChanged();
                }
            }
        }


        public override string ToString()
        {
            return CreatedOn.ToShortDateString() + " " + Onderwerp;
        }
        //{Datum:dd-MM-yyyy}

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(Onderwerp):
                        if (string.IsNullOrWhiteSpace(_onderwerp))
                        {
                            error = "Onderwerp is een verplicht veld.";
                            if (!ErrorList.Contains(nameof(Onderwerp)))
                            {
                                ErrorList.Add(nameof(Onderwerp));
                            }
                        }
                        else if (_onderwerp.Length > 50)
                        {
                            error = "Onderwerp mag niet langer zijn dan 50 tekens.";
                            if (!ErrorList.Contains(nameof(Onderwerp)))
                            {
                                ErrorList.Add(nameof(Onderwerp));
                            }
                        }
                        else
                        {
                            ErrorList.Remove(nameof(Onderwerp));
                        }
                        return error;

                    case nameof(Notitie):
                        if (string.IsNullOrWhiteSpace(_notitie))
                        {
                            error = "Notitie is een verplicht veld.";
                            if (!ErrorList.Contains(nameof(Notitie)))
                            {
                                ErrorList.Add(nameof(Notitie));
                            }
                        }
                        else if (_notitie.Length > 100000)
                        {
                            error = "Notitie mag niet langer zijn dan 5 tekens.";
                            if (!ErrorList.Contains(nameof(Notitie)))
                            {
                                ErrorList.Add(nameof(Notitie));
                            }
                        }
                        else
                        {
                            ErrorList.Remove(nameof(Notitie));
                        }
                        return error;

                    default:
                        return null;
                }
            }
        }
    }
}