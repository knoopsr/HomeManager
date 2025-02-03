using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Todo
{
    public class clsTodoBijlageM : clsCommonModelPropertiesBase, IDataErrorInfo
    {
        private int _todoBijlageID;
        public int TodoBijlageID
        {
            get { return _todoBijlageID; }
            set
            {
                _todoBijlageID = value;
                OnPropertyChanged();
            }
        }

        private int _todoID;
        public int TodoID
        {
            get { return _todoID; }
            set
            {
                _todoID = value;
                OnPropertyChanged();
            }
        }

        private byte[] _bijlage;
        public byte[] Bijlage
        {
            get { return _bijlage; }
            set
            {
                _bijlage = value;
                OnPropertyChanged();
            }
        }

        private string _bijlageNaam;
        public string BijlageNaam
        {
            get { return _bijlageNaam; }
            set
            {
                _bijlageNaam = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return BijlageNaam;
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(BijlageNaam):
                        if (string.IsNullOrWhiteSpace(_bijlageNaam))
                        {
                            error = "Bijlage naam is een verplicht veld.";
                            if (ErrorList.Contains(nameof(BijlageNaam)) == false)
                            {
                                ErrorList.Add(nameof(BijlageNaam));
                            }
                        }
                        break;
                }
                return error;
            }
        }

    }
}
