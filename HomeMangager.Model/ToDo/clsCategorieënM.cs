using HomeManager.Common;
using System.ComponentModel;

namespace HomeManager.Model.Todo;

public class clsCategorieënM : clsCommonModelPropertiesBase, IDataErrorInfo
{
    private int _toDoCategorieënID;
    public int ToDoCategorieID
    {
        get
        {
            return _toDoCategorieënID;
        }
        set
        {
            _toDoCategorieënID = value;
            OnPropertyChanged();
        }
    }

    private string _toDoCategorieën;
    public string ToDoCategorie
    {
        get
        {
            return _toDoCategorieën;
        }
        set
        {
            if (_toDoCategorieën != value)
            {
                if (_toDoCategorieën != null)
                {
                    IsDirty = true;
                }
                _toDoCategorieën = value;
                OnPropertyChanged();
            }
        }
    }

    public override string ToString()
    {
        return ToDoCategorie;
    }

    public string this[string columnName]
    {
        get
        {
            string error = string.Empty;
            switch (columnName)
            {
                case nameof(ToDoCategorie):
                    if (string.IsNullOrWhiteSpace(_toDoCategorieën))
                    {
                        error = "Categorie is een verplicht veld.";
                        if (ErrorList.Contains(nameof(ToDoCategorie)) == false)
                        {
                            ErrorList.Add(nameof(ToDoCategorie));
                        }
                    }
                    else if (_toDoCategorieën.Length > 50)
                    {
                        error = "De voorgestelde categorie is te lang!!!";
                        if (ErrorList.Contains(nameof(ToDoCategorie)) == false)
                        {
                            ErrorList.Add(nameof(ToDoCategorie));

                        }
                    }
                    else
                    {
                        if (ErrorList.Contains(nameof(ToDoCategorie)))
                        {
                            ErrorList.Remove(nameof(ToDoCategorie));
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