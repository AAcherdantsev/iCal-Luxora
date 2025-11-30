using System.Globalization;

namespace iCal.Luxora.Application.UI.Common.Converters;

public class ObjectToIntConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value switch
        {
            int i => i,
            double d => d,
            float f => f,
            _ => 0d
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double d)
            return d;

        return 0;
    }
}