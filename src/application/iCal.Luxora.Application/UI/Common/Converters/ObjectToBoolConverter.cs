using System.Globalization;

namespace iCal.Luxora.Application.UI.Common.Converters;

public class ObjectToBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b)
            return b;
        
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b)
            return b;

        return false;
    }
}