using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Security
{
    public class clsRechtenModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {
        public string Error => null; // Of implementeer met specifieke logica als je validatie wil toepassen

        public string this[string columnName] => throw new NotImplementedException();



        private int _rechtenID;
        public int RechtenID
        {
            get { return _rechtenID; }
            set
            {
                _rechtenID = value;
                OnPropertyChanged();
            }
        }

        private string _rechtenName;
        public string RechtenName
        {
            get { return _rechtenName; }
            set
            {
                _rechtenName = value;
                OnPropertyChanged();
            }
        }

        private int _rechtenCode;
        public int RechtenCode
        {
            get { return _rechtenCode; }
            set
            {
                _rechtenCode = value;
                OnPropertyChanged();
            }
        }

        private int _rechtenCatogorieID;
        public int RechtenCatogorieID
        {
            get { return _rechtenCatogorieID; }
            set
            {
                _rechtenCatogorieID = value;
                OnPropertyChanged();
            }
        }

        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {



                _isChecked = value;
                OnPropertyChanged();
            }
        }


    }
}
