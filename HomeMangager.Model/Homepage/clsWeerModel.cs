using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Homepage
{
    public class clsWeerModel : clsCommonModelPropertiesBase
    {
        private int _accountID;
        public int AccountID
        {
            get { return _accountID; }
            set
            {
                _accountID = value;
                OnPropertyChanged();
            }
        }

        private string _gemeente;
        public string Gemeente
        {
            get { return _gemeente; }
            set
            {
                _gemeente = value;
                OnPropertyChanged();
            }
        }

        private DateTime _datum;
        public DateTime Datum
        {
            get { return _datum; }
            set
            {
                _datum = value;
                OnPropertyChanged();
            }
        }

        private double _temperatuur;
        public double Temperatuur
        {
            get { return _temperatuur; }
            set
            {
                _temperatuur = value;
                OnPropertyChanged();
            }
        }

        private string _omschrijving;
        public string Omschrijving
        {
            get { return _omschrijving; }
            set
            {
                _omschrijving = value;
                OnPropertyChanged();
            }
        }

        private string _icoon;
        public string Icoon
        {
            get { return _icoon; }
            set
            {
                _icoon = value;
                OnPropertyChanged();
            }
        }
        public string DatumTijdString => Datum.ToString("dd MMM yyyy HH:mm");
    }
}
