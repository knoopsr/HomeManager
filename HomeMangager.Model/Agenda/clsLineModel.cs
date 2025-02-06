using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Agenda
{
    public class clsLineModel : clsCommonModelPropertiesBase
    {
        private double _x1;
        public double X1
        {
            get => _x1;
            set
            {
                _x1 = value;
                OnPropertyChanged();
            }
        }

        private double _y1;
        public double Y1
        {
            get => _y1;
            set
            {
                _y1 = value;
                OnPropertyChanged();
            }
        }

        private double _x2;
        public double X2
        {
            get => _x2;
            set
            {
                _x2 = value;
                OnPropertyChanged();
            }
        }

        private double _y2;
        public double Y2
        {
            get => _y2;
            set
            {
                _y2 = value;
                OnPropertyChanged();
            }
        }

        public string Color { get; set; } = "Black"; // Optioneel, voor dynamische kleur
        public double Thickness { get; set; } = 2;   // Optioneel, voor dikte van de lijn
    }
}
