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
    /// Converts a <see cref="DateTime"/> value to a corresponding <see cref="Brush"/> based on its relation to the current date.
    /// </summary>
    /// <remarks>
    /// Used in UI elements to visually differentiate future, present, and past dates by changing the foreground color.
    /// - Future dates: Green
    /// - Today's date: White
    /// - Past dates: Red
    /// </remarks>
    public class clsDateToForegroundConverter : IValueConverter
    {
        /// <summary>
        /// Converts a <see cref="DateTime"/> value to a <see cref="Brush"/> indicating whether the date is in the past, today, or in the future.
        /// </summary>
        /// <param name="value">The value to convert. Expected to be a <see cref="DateTime"/>.</param>
        /// <param name="targetType">The type of the binding target property (expected to be <see cref="Brush"/>).</param>
        /// <param name="parameter">An optional parameter (not used).</param>
        /// <param name="culture">The culture to use in the converter (not used).</param>
        /// <returns>
        /// A <see cref="Brush"/>:
        /// - Green for future dates,
        /// - White for today's date,
        /// - Red for past dates,
        /// - Black as fallback if value is not a valid <see cref="DateTime"/>.
        /// </returns>
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

        /// <summary>
        /// This method is not implemented because one-way binding is expected for this converter.
        /// </summary>
        /// <param name="value">The value that is being converted back (not used).</param>
        /// <param name="targetType">The type to convert to (not used).</param>
        /// <param name="parameter">An optional parameter (not used).</param>
        /// <param name="culture">The culture to use in the converter (not used).</param>
        /// <returns>Nothing. Always throws <see cref="NotImplementedException"/>.</returns>
        /// <exception cref="NotImplementedException">Thrown always since reverse conversion is not supported.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
