using System.Globalization;

namespace AIPlayground.UI.PresentationLayer.Converters
{
    /// <summary>
    /// Converter to check if a string is not null or empty
    /// </summary>
    public class StringIsNotEmptyConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return !string.IsNullOrWhiteSpace(value as string);
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
