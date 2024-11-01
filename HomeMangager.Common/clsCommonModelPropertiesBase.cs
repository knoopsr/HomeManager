﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Common
{
    public class clsCommonModelPropertiesBase : clsObservable
    {
        private bool _isDirty = false;
        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {

                _isDirty = value;
                OnPropertyChanged();

            }
        }

        private int _mijnSlectedIndex;
        public int MijnSelectedIndex
        {
            get { return _mijnSlectedIndex; }
            set
            {
                _mijnSlectedIndex = value;
                OnPropertyChanged();
            }
        }

        private int _myVisibility;
        public int MyVisibility
        {
            get { return _myVisibility; }
            set
            {
                _myVisibility = value;
                OnPropertyChanged();
            }
        }

        private int _myVisibility_Contrary = 1;
        public int MyVisibility_Contrary
        {
            get { return _myVisibility_Contrary; }
            set
            {
                _myVisibility_Contrary = value;
                OnPropertyChanged();
            }
        }

        private object _controlField;
        public object ControlField
        {
            get { return _controlField; }
            set
            {
                _controlField = value;
                OnPropertyChanged();
            }
        }

        private bool _IsFocused = false;
        public bool IsFocused
        {
            get { return _IsFocused; }
            set
            {
                _IsFocused = value;
                OnPropertyChanged();
            }
        }

        private bool _IsFocusedAfterNew = false;
        public bool IsFocusedAfterNew
        {
            get { return _IsFocusedAfterNew; }
            set
            {
                _IsFocusedAfterNew = value;
                OnPropertyChanged();
            }
        }


        public string ErrorBoodschap { get; set; }

        private List<string> _errorList = new List<string>();
        public List<string> ErrorList
        {
            get { return _errorList; }
            set
            {
                _errorList = value;
            }
        }

        public string Error
        {
            get
            {
                if (ErrorList.Count > 0)
                {
                    return "NOK";
                }
                else
                {
                    return null;
                }
            }
        }
    }

}