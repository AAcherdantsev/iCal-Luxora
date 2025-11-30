using iCal.Luxora.Models.Abstractions;
using iCal.Luxora.Models.Enums;

namespace iCal.Luxora.Application.UI.Selectors;

public class ParameterTemplateSelector : DataTemplateSelector
{
    public DataTemplate BrightnessTemplate { get; set; }
    public DataTemplate VolumeTemplate { get; set; }
    public DataTemplate SliderTemplate { get; set; }
    public DataTemplate ColorTemplate { get; set; }
    public DataTemplate CheckBoxTemplate { get; set; }
    public DataTemplate TextTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var template = new DataTemplate(() => new Label { Text = "Unsupported" });

        if (container is IParameterSettings parameterSettings)
            return parameterSettings.Type switch
            {
                ParameterType.Brightness => BrightnessTemplate,
                ParameterType.Volume => VolumeTemplate,
                ParameterType.Color => ColorTemplate,
                ParameterType.Slider => SliderTemplate,
                ParameterType.CheckBox => CheckBoxTemplate,
                ParameterType.Text => TextTemplate,
                _ => template
            };

        return template;
    }
}