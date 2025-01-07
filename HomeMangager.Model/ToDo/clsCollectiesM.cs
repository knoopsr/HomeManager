using System.ComponentModel;
using HomeManager.Common;

namespace HomeManager.Model.Todo;

public class clsCollectiesM : clsCommonModelPropertiesBase, IDataErrorInfo
{
    private int _toDoCollectieID;
    public int ToDoCollectieID
    {
        get
        {
            return _toDoCollectieID;
        }
        set
        {
            _toDoCollectieID = value;
            OnPropertyChanged();
        }
    }

    private string _toDoCollectie;
    public string ToDoCollectie
    {
        get
        {
            return _toDoCollectie;
        }
        set
        {
            if (_toDoCollectie != value)
            {
                if (_toDoCollectie != null)
                {
                    IsDirty = true;
                }
                _toDoCollectie = value;
                OnPropertyChanged();
            }
        }
    }

    public override string ToString()
    {
        return ToDoCollectie;
    }
    public string this[string columnName]
    {
        get
        {
            string error = string.Empty;
            switch (columnName)
            {
                case nameof(ToDoCollectie):
                    if (string.IsNullOrWhiteSpace(_toDoCollectie))
                    {
                        error = "Budget Categorie is een verplicht veld.";
                        if (ErrorList.Contains(nameof(ToDoCollectie)) == false)
                        {
                            ErrorList.Add(nameof(ToDoCollectie));
                        }
                    }
                    else if (_toDoCollectie.Length > 50)
                    {
                        error = "De voorgestelde categorie is te lang!!!";
                        if (ErrorList.Contains(nameof(ToDoCollectie)) == false)
                        {
                            ErrorList.Add(nameof(ToDoCollectie));

                        }
                    }
                    else
                    {
                        if (ErrorList.Contains(nameof(ToDoCollectie)))
                        {
                            ErrorList.Remove(nameof(ToDoCollectie));
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