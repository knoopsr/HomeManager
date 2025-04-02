using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Personen
{
    public class clsEmailVerzendenModel : clsCommonModelPropertiesBase
    {
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

        private int _emailID;
        public int EmailID
        {
            get
            {
                return _emailID;
            }
            set
            {
                _emailID = value;
                OnPropertyChanged();
            }
        }

        private string _ontvanger;

        public string Ontvanger
        {
            get
            {
                return _ontvanger;
            }
            set
            {
                if (_ontvanger != value)
                {
                    if (_ontvanger != null)
                    {
                        IsDirty = true;
                    }
                }
                _ontvanger = value;
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

        private string _bericht;
        public string Bericht
        {
            get
            {
                return _bericht;
            }
            set
            {
                if (_bericht != value)
                {
                    if (_bericht != null)
                    {
                        IsDirty = true;
                    }
                }
                _bericht = value;
                OnPropertyChanged();
            }
        }
    }
}
