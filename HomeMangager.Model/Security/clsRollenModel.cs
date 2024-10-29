using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Security
{
    public class clsRollenModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {
        public string this[string columnName] => throw new NotImplementedException();

        private int _rolID;
        public int RolID
        {
            get { return _rolID; }
            set
            {

                    _rolID = value;
                    OnPropertyChanged();
                
            }
        }

        private string _rolName;
        public string RolName
        {
            get { return _rolName; }
            set
            {
                if (_rolName != value)
                {
                    if (_rolName != null)
                    {
                        IsDirty = true;
                    }
                }
                    _rolName = value;
                OnPropertyChanged();
            }
        }

        private string _rechten;
        public string Rechten
        {
            get { return _rechten; }
            set
            {
                if (_rechten != value)
                {
                    if (_rechten != null)
                    {
                        IsDirty = true;
                    }
                }
                _rechten = value;
                OnPropertyChanged();
            }
        }

            }
}
