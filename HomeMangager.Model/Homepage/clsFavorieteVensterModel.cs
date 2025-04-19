using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Homepage
{
    public  class clsFavorieteVensterModel : clsCommonModelPropertiesBase
    {
        private int _FavorietID;
        public int FavorietID
        {
            get => _FavorietID;
            set
            {
                _FavorietID = value;
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

        private string _VensterNaam;
        public string VensterNaam
        {
            get => _VensterNaam;
            set
            {
                _VensterNaam = value;
                OnPropertyChanged();
            }
        }
        public DateTime CreatedOn { get; set; }
        public DateTime? ChangedOn { get; set; }
    }
}
