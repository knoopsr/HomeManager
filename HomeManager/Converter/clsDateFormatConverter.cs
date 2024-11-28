using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HomeManager.Converter
{
    public class clsDateFormatConverter : IValueConverter
    {
        // Convert DateTime/DateOnly to string in DDMMYYYY format
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                // Formatteer de datum als DDMMYYYY
                return dateTime.ToString("dd/MM/yyyy");
            }
            else if (value is DateOnly dateOnly)
            {
                // Formatteer de datum als DDMMYYYY
                return dateOnly.ToString("dd/MM/yyyy");
            }

            // Als de input geen geldig datumtype is, geef een lege string of standaardwaarde
            return string.Empty;
        }

        // Convert back is optioneel, afhankelijk van je scenario
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string dateString && dateString.Length == 8)
            {
                try
                {
                    // Parse de string DDMMYYYY terug naar DateTime
                    int day = int.Parse(dateString.Substring(0, 2));
                    int month = int.Parse(dateString.Substring(2, 2));
                    int year = int.Parse(dateString.Substring(4, 4));

                    if (targetType == typeof(DateTime))
                    {
                        return new DateTime(year, month, day);
                    }
                    else if (targetType == typeof(DateOnly))
                    {
                        return new DateOnly(year, month, day);
                    }
                }
                catch
                {
                    // Parsing mislukt, retourneer de standaardwaarde
                    return targetType == typeof(DateTime) ? DateTime.MinValue : default(DateOnly);
                }
            }

            // Als de input geen geldig formaat heeft, geef standaardwaarde
            return targetType == typeof(DateTime) ? DateTime.MinValue : default(DateOnly);
        }
    }
}
