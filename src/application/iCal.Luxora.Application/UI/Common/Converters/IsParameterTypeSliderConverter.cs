using System.Globalization;
using iCal.Luxora.Models.Enums;

namespace iCal.Luxora.Application.UI.Common.Converters;

public class IsParameterTypeSliderConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ParameterType parameterType)
        {
            return parameterType is ParameterType.Slider or ParameterType.Brightness or ParameterType.Volume;
        }
        return false;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}