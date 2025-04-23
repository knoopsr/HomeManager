using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Homepage
{
    public class clsSnelkoppelingModel : clsCommonModelPropertiesBase
    {
        private int _SnelkoppelingID;
        public int SnelkoppelingID
        {
            get => _SnelkoppelingID;
            set
            {
                _SnelkoppelingID = value;
                OnPropertyChanged();
            }
        }

        private int _AccountID;
        public int AccountID
        {
            get => _AccountID;
            set
            {
                _AccountID = value;
                OnPropertyChanged();
            }
        }

        private string _Naam;
        public string Naam
        {
            get => _Naam;
            set
            {
                _Naam = value;
                OnPropertyChanged();
            }
        }

        private string _Pad;
        public string Pad
        {
            get => _Pad;
            set
            {
                _Pad = value;
                OnPropertyChanged();
            }
        }

        private string _Type; 
        public string Type
        {
            get => _Type;
            set
            {
                _Type = value;
                OnPropertyChanged();
            }
        }
        public DateTime CreatedOn { get; set; }
        public DateTime? ChangedOn { get; set; }
    }
}
