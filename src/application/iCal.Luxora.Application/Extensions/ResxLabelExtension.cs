using System.Globalization;
using iCal.Luxora.Application.Resources.Strings;

namespace iCal.Luxora.Application.Extensions;

[ContentProperty("Source")]
public class ResxLabelExtension : IMarkupExtension
{
    public string Source { get; set; }

    public object ProvideValue(IServiceProvider serviceProvider)
    {
        object value = null;

        if (Source != null)
        {
            var type = typeof(AppResources);
            var property = type.GetProperty(Source);

            if (property != null) value = property.GetValue(null, null);
        }

        return value;
    }
}

public static class ResxLabel
{
    public static string Get(string key)
    {
        return AppResources.ResourceManager.GetString(key, CultureInfo.CurrentUICulture);
    }
}