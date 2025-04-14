using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace HomeManager.Converter
{
    /// <summary>
    /// Date to foreground converter
    /// </summary>
    public class clsDateToForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime date)
            {
                DateTime today = DateTime.Today;

                if (date > today)
                {
                    // Future date: Green
                    return Brushes.Green;
                }
                else if (date == today)
                {
                    // Today: White
                    return Brushes.White;
                }
                else
                {
                    // Past date: Red
                    return Brushes.Red;
                }
            }

            // Debug message
            Debug.WriteLine("Converter called with value: " + value);

            // Default color if the value is not a DateTime
            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
