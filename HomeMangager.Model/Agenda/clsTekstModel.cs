using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Agenda
{
    public class clsTekstModel : clsCommonModelPropertiesBase
    {
        private string _tekst;
        public string Tekst
        {
            get { return _tekst; }
            set
            {
                _tekst = value;
                OnPropertyChanged();
            }
        }

        private double _top;
        public double Top
        {
            get { return _top; }
            set
            {
                _top = value;
                OnPropertyChanged();
            }
        }
        private double _left;
        public double Left
        {
            get { return _left; }
            set
            {
                _left = value;
                OnPropertyChanged();
            }
        }
        private double _width;
        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                OnPropertyChanged();
            }
        }
        private double _height;
        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged();
            }
        }
        private double _fontsize;
        public double FontSize
        {
            get { return _fontsize; }
            set
            {
                _fontsize = value;
                OnPropertyChanged();
            }
        }

        private string _fontfamily;
        public string FontFamily
        {
            get { return _fontfamily; }
            set
            {
                _fontfamily = value;
                OnPropertyChanged();
            }
        }
        private string _color;
        public string Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged();
            }
        }

    }
}
