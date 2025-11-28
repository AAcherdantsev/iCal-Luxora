using System.Globalization;
using iCal.Luxora.Models.Enums;

namespace iCal.Luxora.Application.UI.Common.Converters;

public class ParameterTypeToImageConverter : IValueConverter
{
    private static readonly Dictionary<ParameterType, string> ParameterTypeToImage = new()
    {
        { ParameterType.Brightness, "brightness" },
        { ParameterType.Volume, "sound_on" },
        { ParameterType.Slider, "slider" },
        { ParameterType.Color, "color" },
        { ParameterType.CheckBox, "" }, // no image for checkboxes
        { ParameterType.Text, "text" },

    };
    
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ParameterType parameterType)
        {
            return ParameterTypeToImage[parameterType];
        }
        return string.Empty;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}