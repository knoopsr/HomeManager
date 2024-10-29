using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMangager.Common
{
    public class clsCommonModelPropertyBase : clsObservable
    {
        private bool _IsDirty = false;

        public bool IsDirty
        {
            get { return _IsDirty; }
            set
            {
                _IsDirty = value;
                OnPropertyChanged();
            }
        }

        private int _mijnSelectedIndex;

        public int MijnSelectedIndex
        {
            get { return _mijnSelectedIndex; }

            set
            {
                _mijnSelectedIndex = value;
                OnPropertyChanged();
            }
        }

        private int _MyVisibility;

        public int MyVisibility
        {
            get { return _MyVisibility; }
            set { _MyVisibility = value; OnPropertyChanged(); }
        }
        private object controlField;

        public object ControlField
        {
            get { return controlField; }
            set { controlField = value; OnPropertyChanged(); }
        }
        public string ErrorBoodschap { get; set; }


        private List<string> errorList = new List<string>();
        public List<string> ErrorList
        {
            get
            {
                return errorList;
            }
            set
            {
                errorList = value;
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

