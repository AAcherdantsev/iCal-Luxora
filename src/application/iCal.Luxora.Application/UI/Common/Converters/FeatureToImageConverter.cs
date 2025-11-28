using System.Globalization;
using iCal.Luxora.Models.Enums;

namespace iCal.Luxora.Application.UI.Common.Converters;

public class FeatureToImageConverter : IValueConverter
{
    private static readonly Dictionary<Feature, string> FeatureToImage = new()
    {
        { Feature.Bluetooth, "bluetooth" },
        { Feature.WiFi, "wifi" },
        { Feature.Music, "music" },
        { Feature.Games, "games" },
        { Feature.SdCard, "sd_card" },
        { Feature.Microphone, "microphone" },
        { Feature.Paint, "draw_brush_reflection" },
        { Feature.Battery, "battery_full" },
        { Feature.AutoBrightness, "auto_brightness" },
    };
    
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Feature feature)
        {
            return FeatureToImage[feature];
        }
        return string.Empty;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}