using HomeManager.Common;
using System.ComponentModel;

namespace HomeManager.Model.Agenda
{
    public class clsAgendaItemModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {

        public override string ToString()
        {
            return AgendaTitle + " op " + AgendaDate.ToString("dd-MM-yyyy");
        }


        private int _agendaID;
        public int AgendaID
        {
            get { return _agendaID; }
            set
            {
                _agendaID = value;
                OnPropertyChanged();
            }
        }

        private string _agendaTitle;
        public string AgendaTitle
        {
            get { return _agendaTitle; }
            set
            {
                if (_agendaTitle != value)
                {
                    if (_agendaTitle != null)
                    {
                        IsDirty = true;
                    }
                }
                _agendaTitle = value;
                OnPropertyChanged();
            }
        }
        private string _agendaDescription;
        public string AgendaDescription
        {
            get { return _agendaDescription; }
            set
            {
                if (_agendaDescription != value)
                {
                    if (_agendaDescription != null)
                    {
                        IsDirty = true;
                    }
                }
                _agendaDescription = value;
                OnPropertyChanged();
            }
        }
        private int _agendaCategoryID;
        public int AgendaCategoryID
        {
            get { return _agendaCategoryID; }
            set
            {
                if (_agendaCategoryID != value)
                {
                    IsDirty = true;
                }
                _agendaCategoryID = value;
                OnPropertyChanged();
            }
        }
        private DateTime _agendaDate;
        public DateTime AgendaDate
        {
            get { return _agendaDate; }
            set
            {
                if (_agendaDate != value)
                {
                    IsDirty = true;
                }
                _agendaDate = value;
                OnPropertyChanged();
            }
        }




        private TimeSpan _agendaBeginTime;
        public TimeSpan AgendaBeginTime
        {
            get { return _agendaBeginTime; }
            set
            {
                if (_agendaBeginTime != value)
                {
                    IsDirty = true;
                    _agendaBeginTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private TimeSpan _agendaEndTime;
        public TimeSpan AgendaEndTime
        {
            get { return _agendaEndTime; }
            set
            {
                if (_agendaEndTime != value)
                {
                    IsDirty = true;
                    _agendaEndTime = value;
                    OnPropertyChanged();
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

        private double _canvasTop;
        public double CanvasTop
        {
            get { return _canvasTop; }
            set
            {
                _canvasTop = value;
                OnPropertyChanged();
            }
        }

        private double _canvasLeft;
        public double CanvasLeft
        {
            get { return _canvasLeft; }
            set
            {
                _canvasLeft = value;
                OnPropertyChanged();
            }
        }

        private double _height;
        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged();
            }
        }

        private double _width;
        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                OnPropertyChanged();
            }
        }

        private string _agendaColor;
        public string AgendaColor
        {
            get { return _agendaColor; }
            set
            {
                _agendaColor = value;
                OnPropertyChanged();
            }
        }

        private string _agendaBorderColor = "Black";
        public string AgendaBorderColor
        {
            get { return _agendaBorderColor; }
            set
            {
                _agendaBorderColor = value;
                OnPropertyChanged();
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(AgendaTitle):
                        if (string.IsNullOrWhiteSpace(AgendaTitle))
                        {
                            error = "AgendaTitle is verplicht veld.";
                            if (ErrorList.Contains(nameof(AgendaTitle)) == false)
                            {
                                ErrorList.Add(nameof(AgendaTitle));
                            }
                        }
                        else if (AgendaTitle.Length > 100)
                        {
                            error = "WachtwoordNaam mag niet langer zijn dan 50 karakters.";
                            if (ErrorList.Contains(nameof(AgendaTitle)) == false)
                            {
                                ErrorList.Add(nameof(AgendaTitle));
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains(nameof(AgendaTitle)))
                            {
                                ErrorList.Remove(nameof(AgendaTitle));
                            }
                        }
                        return error;

                    case nameof(AgendaDescription):
                        if (AgendaDescription.Length > 1000)
                        {
                            error = "AgendaDescription mag niet langer zijn dan 1000 karakters.";
                            if (ErrorList.Contains(nameof(AgendaDescription)) == false)
                            {
                                ErrorList.Add(nameof(AgendaDescription));
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains(nameof(AgendaDescription)))
                            {
                                ErrorList.Remove(nameof(AgendaDescription));
                            }
                        }
                        return error;


                    //case nameof(AgendaBeginDateTime):
                    //    if (AgendaBeginDateTime == DateTime.MinValue)
                    //    {
                    //        error = "AgendaBeginDateTime is een verplicht veld en moet een geldige datum en tijd bevatten.";
                    //        if (!ErrorList.Contains(nameof(AgendaBeginDateTime)))
                    //        {
                    //            ErrorList.Add(nameof(AgendaBeginDateTime));
                    //        }
                    //    }
                    //    //else if (AgendaBeginDateTime < DateTime.Now)
                    //    //{
                    //    //    error = "AgendaBeginDateTime moet in de toekomst liggen.";
                    //    //    if (!ErrorList.Contains(nameof(AgendaBeginDateTime)))
                    //    //    {
                    //    //        ErrorList.Add(nameof(AgendaBeginDateTime));
                    //    //    }
                    //    //}

                    //    else
                    //            {
                    //        if (ErrorList.Contains(nameof(AgendaBeginDateTime)))
                    //        {
                    //            ErrorList.Remove(nameof(AgendaBeginDateTime));
                    //        }
                    //    }
                    //    return error;

                    //case nameof(AgendaEndDateTime):
                    //    if (AgendaEndDateTime == DateTime.MinValue)
                    //    {
                    //        error = "AgendaEndDateTime is een verplicht veld en moet een geldige datum en tijd bevatten.";
                    //        if (!ErrorList.Contains(nameof(AgendaEndDateTime)))
                    //        {
                    //            ErrorList.Add(nameof(AgendaEndDateTime));
                    //        }
                    //    }
                    //    else if (AgendaEndDateTime <= AgendaBeginDateTime)
                    //    {
                    //        error = "AgendaEndDateTime moet na de AgendaBeginDateTime liggen.";
                    //        if (!ErrorList.Contains(nameof(AgendaEndDateTime)))
                    //        {
                    //            ErrorList.Add(nameof(AgendaEndDateTime));
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (ErrorList.Contains(nameof(AgendaEndDateTime)))
                    //        {
                    //            ErrorList.Remove(nameof(AgendaEndDateTime));
                    //        }
                    //    }
                    //    return error;


                    case nameof(AgendaBeginTime):
                        if (AgendaBeginTime == null)
                        {
                            error = "AgendaBeginTime is een verplicht veld en moet een geldige tijd bevatten.";
                            if (!ErrorList.Contains(nameof(AgendaBeginTime)))
                            {
                                ErrorList.Add(nameof(AgendaBeginTime));
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains(nameof(AgendaBeginTime)))
                            {
                                ErrorList.Remove(nameof(AgendaBeginTime));
                            }
                        }
                        return error;

                    case nameof(AgendaEndTime):
                        if (AgendaEndTime == null)
                        {
                            error = "AgendaEndTime is een verplicht veld en moet een geldige tijd bevatten.";
                            if (!ErrorList.Contains(nameof(AgendaEndTime)))
                            {
                                ErrorList.Add(nameof(AgendaEndTime));
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains(nameof(AgendaEndTime)))
                            {
                                ErrorList.Remove(nameof(AgendaEndTime));
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
