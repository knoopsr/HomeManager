using HomeManager.Common;
using System.ComponentModel;
using System.Drawing;

namespace HomeManager.Model.Todo;

public class clsKleurenM : clsCommonModelPropertiesBase, IDataErrorInfo
{
    private int _toDoColorID;
    public int ToDoColorID
    {
        get
        {
            return _toDoColorID;
        }
        set
        {
            _toDoColorID = value;
            OnPropertyChanged();
        }
    }

    private string _toDoColor;
    public string ToDoColor
    {
        get
        {
            return _toDoColor;
        }
        set
        {
            if (_toDoColor != value)
            {
                if (_toDoColor != null)
                {
                    IsDirty = true;
                }
                _toDoColor = value;
                OnPropertyChanged();
            }
        }
    }

    public override string ToString()
    {
        return ToDoColor;
    }

    
    public string Name { get; set; }
    public Color Color { get; set; }
    

    public string this[string columnName]
    {
        get
        {
            string error = string.Empty;
            switch (columnName)
            {
                case nameof(ToDoColor):
                    if (string.IsNullOrWhiteSpace(_toDoColor))
                    {
                        error = "Budget Categorie is een verplicht veld.";
                        if (ErrorList.Contains(nameof(ToDoColor)) == false)
                        {
                            ErrorList.Add(nameof(ToDoColor));
                        }
                    }
                    else if (_toDoColor.Length > 50)
                    {
                        error = "De voorgestelde categorie is te lang!!!";
                        if (ErrorList.Contains(nameof(ToDoColor)) == false)
                        {
                            ErrorList.Add(nameof(ToDoColor));

                        }
                    }
                    else
                    {
                        if (ErrorList.Contains(nameof(ToDoColor)))
                        {
                            ErrorList.Remove(nameof(ToDoColor));
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