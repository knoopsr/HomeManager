using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HomeManager.Helpers
{
	public class clsDagboekCustomColor
	{
		private string _colorName;

		public string ColorName
		{
			get { return _colorName; }
			set { _colorName = value; }
		}

		private Color _myColor;

		public Color MyColor
		{
			get { return _myColor; }
			set { _myColor = value; }
		}

		//bush is needed for ui
		public SolidColorBrush MyBrush => new SolidColorBrush(MyColor);



        public override string ToString()
        {
            return ColorName;
        }
    }
}
