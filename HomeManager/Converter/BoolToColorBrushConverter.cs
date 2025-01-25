using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace HomeManager.Converter
{
    public class BoolToColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isKlaar)
            {
                if (parameter?.ToString() == "Background")
                {
                    return isKlaar ? Brushes.LavenderBlush : Brushes.MistyRose;
                }
                else if (parameter?.ToString() == "Border")
                {
                    return isKlaar ? Brushes.Green : Brushes.Red;
                }
            }
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
