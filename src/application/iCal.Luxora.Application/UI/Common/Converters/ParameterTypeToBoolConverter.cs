using System.Globalization;
using iCal.Luxora.Models.Enums;

namespace iCal.Luxora.Application.UI.Common.Converters;

public class ParameterTypeToBoolConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ParameterType parameterType)
        {
            var result = parameterType is ParameterType.Color
                or ParameterType.Brightness
                or ParameterType.Slider
                or ParameterType.Volume
                or ParameterType.Text;
            
            if (parameter is bool isInverted)
            {
                return isInverted ? !result : result;
            }
            return result;
        }

        return false;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}