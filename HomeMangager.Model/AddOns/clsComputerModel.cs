using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.AddOns
{
    public class clsComputerModel : clsCommonModelPropertiesBase
    {

        private string _computerNaam;
        public string ComputerNaam
        {
            get
            {
                return _computerNaam;
            }
            set
            {
                if (_computerNaam != value)
                {
                    if (_computerNaam != null)
                    {
                        IsDirty = true;
                    }
                }
                _computerNaam = value;
                OnPropertyChanged();
            }
        }

        private string _ipAdres;
        public string IpAdres
        {
            get
            {
                return _ipAdres;
            }
            set
            {
                if (_ipAdres != value)
                {
                    if (_ipAdres != null)
                    {
                        IsDirty = true;
                    }
                }
                _ipAdres = value;
                OnPropertyChanged();
            }
        }

        private string _macAdres;
        public string MacAdres
        {
            get
            {
                return _macAdres;
            }
            set
            {
                if (_macAdres != value)
                {
                    if (_macAdres != null)
                    {
                        IsDirty = true;
                    }
                }
                _macAdres = value;
                OnPropertyChanged();
            }
        }

        private int _poort;
        public int Poort
        {
            get
            {
                return _poort;
            }
            set
            {
                _poort = value;
                OnPropertyChanged();
            }
        }
    }
}
