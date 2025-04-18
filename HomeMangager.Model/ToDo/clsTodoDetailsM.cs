﻿using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Todo
{
    public class clsTodoDetailsM : clsCommonModelPropertiesBase, IDataErrorInfo
    {
        private int _toDoDetailID;
        public int TodoDetailID
        {
            get
            {
                return _toDoDetailID;
            }
            set
            {
                _toDoDetailID = value;
                OnPropertyChanged();
            }
        }

        private int _toDoID;
        public int TodoID
        {
            get
            {
                return _toDoID;
            }
            set
            {
                _toDoID = value;
                OnPropertyChanged();
            }
        }

        private string _toDoDetail;
        public string TodoDetail
        {
            get { return _toDoDetail; }
            set
            {
                if (_toDoDetail != value)
                {
                    _toDoDetail = value;
                    IsDirty = true;  // Markeer dat er een wijziging is geweest
                    OnPropertyChanged();  // Informeer de UI dat de waarde is veranderd
                    //CommandManager.InvalidateRequerySuggested();  // Laat WPF de Save-knop opnieuw evalueren
                }
            }
        }

        private bool _isKlaar;
        public bool IsKlaar
        {
            get
            {
                return _isKlaar;
            }
            set
            {
                _isKlaar = value;
                OnPropertyChanged();
            }
        }

        private int? _volgorde;
        public int? Volgorde
        {
            get
            {
                return _volgorde;
            }
            set
            {
                _volgorde = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return TodoDetail;
        }
        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(TodoDetail):
                        if (string.IsNullOrWhiteSpace(_toDoDetail))
                        {
                            error = "Detail is een verplicht veld.";
                            if (!ErrorList.Contains(nameof(TodoDetail)))
                            {
                                ErrorList.Add(nameof(TodoDetail));
                            }
                        }
                        else if (_toDoDetail.Length > 50)
                        {
                            error = "Detail mag maximaal 50 karakters bevatten.";
                            if (!ErrorList.Contains(nameof(TodoDetail)))
                            {
                                ErrorList.Add(nameof(TodoDetail));
                            }
                        }
                        else
                        {
                            // Verwijder fout als de invoer correct is
                            ErrorList.Remove(nameof(TodoDetail));
                        }
                        break;
                }
                return error;
            }
        }
    }
}
