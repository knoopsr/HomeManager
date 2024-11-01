using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Common;

namespace HomeManager.Model.Security
{
    public class clsRechtenCatogorieModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {
        public string Error => null;


        public clsRechtenCatogorieModel()
        {
            Rechten = new ObservableCollection<clsRechtenModel>();
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

        private string _catogorieNaam;
        public string CatogorieNaam
        {
            get { return _catogorieNaam; }
            set
            {
                _catogorieNaam = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<clsRechtenModel> _rechten;
        public ObservableCollection<clsRechtenModel> Rechten
        {
            get { return _rechten; }
            set
            {
           

                _rechten = value;
                OnPropertyChanged();
            }
        }



        private bool? _isChecked;
        public bool? IsChecked
        {
            get { return _isChecked; }
            set
            {



                _isChecked = value;
                OnPropertyChanged();


                if (_isChecked.HasValue)
                {
                    foreach (var item in Rechten)
                    {
                        item.IsChecked = _isChecked.Value;
                    }
                }

            }
        }



        public string this[string columnName] => throw new NotImplementedException();
    }
}
