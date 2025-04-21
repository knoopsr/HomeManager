using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace HomeManager.Converter
{
    /// <summary>
    /// A value converter that converts a string key to a corresponding <see cref="Brush"/> from the application's resources.
    /// This converter is typically used for binding string keys to brush resources in XAML.
    /// </summary>
    public class clsStringToBrushConverter : IValueConverter
    {
        /// <summary>
        /// Converts a string key to a <see cref="Brush"/> by looking it up in the application resources.
        /// </summary>
        /// <param name="value">The string key used to find the corresponding <see cref="Brush"/> in the application resources.</param>
        /// <param name="targetType">The target type of the conversion. This should be <see cref="Brush"/>.</param>
        /// <param name="parameter">An optional parameter used for additional configuration (not used in this case).</param>
        /// <param name="culture">The culture used for formatting (not used in this case).</param>
        /// <returns>
        /// A <see cref="Brush"/> corresponding to the string key from the application resources,
        /// or <see cref="DependencyProperty.UnsetValue"/> if the key is not found or the value is not a string.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var key = value as string;
            return key != null ? Application.Current.Resources[key] as Brush : DependencyProperty.UnsetValue;
        }

        /// <summary>
        /// This method is not used for this converter. It always returns <see cref="Binding.DoNothing"/>.
        /// </summary>
        /// <param name="value">The value to convert back (not used).</param>
        /// <param name="targetType">The target type of the conversion (not used).</param>
        /// <param name="parameter">An optional parameter used for additional configuration (not used).</param>
        /// <param name="culture">The culture used for formatting (not used).</param>
        /// <returns>
        /// Always returns <see cref="Binding.DoNothing"/>.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
