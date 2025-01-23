using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HomeManager.Agenda.Converter
{
    public class clsTimeOnlyConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeOnly time)
            {
                return time.ToString("HH:mm"); // Gebruik het juiste formaat
            }
            return string.Empty; // Voor null of niet-herkende waarden
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(value as string))
            {
                return null; // Of TimeOnly.MinValue, afhankelijk van je voorkeur
            }

            if (TimeOnly.TryParse(value.ToString(), out TimeOnly result))
            {
                return result;
            }

            return Binding.DoNothing; // Of geef een standaardwaarde terug
        }
    }
}
