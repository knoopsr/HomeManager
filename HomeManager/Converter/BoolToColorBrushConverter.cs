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
            if (value is bool boolValue)
            {
                // Controleer de parameter om te bepalen welke logica moet worden gebruikt
                switch (parameter?.ToString())
                {
                    case "Background":
                        return boolValue ? Brushes.LavenderBlush : Brushes.MistyRose;
                    case "Border":
                        return boolValue ? Brushes.Green : Brushes.Red;
                    case "Belangrijk":
                        return boolValue ? Brushes.Red : Brushes.Gray;
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
