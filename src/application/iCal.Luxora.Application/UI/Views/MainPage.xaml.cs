using iCal.Luxora.Models.Abstractions;
using iCal.Luxora.Models.Enums;
using iCal.Luxora.Models.Parameters;

namespace iCal.Luxora.Application.UI.Views;

public partial class MainPage : ContentPage
{
    public List<IParameterSettings> ParameterSettings { get; set; } = new List<IParameterSettings>()
    {
        new ParameterSettings<int>()
        {
            Name = "Brightness Parameter",
            CurrentValue = 3,
            Type = ParameterType.Brightness,
            DefaultValue = 50,
            MinValue = 0,
            MaxValue = 100,
            Id = 1,
        },
        new ParameterSettings<int>()
        {
            Name = "Volume Parameter",
            CurrentValue = 3,
            Type = ParameterType.Volume,
            DefaultValue = 2,
            MinValue = 0,
            MaxValue = 10,
            Id = 1,
        },
        new ParameterSettings<int>()
        {
            Name = "Slider Parameter",
            CurrentValue = 3,
            Type = ParameterType.Slider,
            DefaultValue = 50,
            MinValue = 0,
            MaxValue = 10,
            Id = 1,
        },
        new ParameterSettings<bool>()
        {
            Name = "Check box Parameter",
            CurrentValue = true,
            Type = ParameterType.CheckBox,
            DefaultValue = false,
            Id = 1,
        },
        new ParameterSettings<int>()
        {
            Name = "Color Parameter",
            CurrentValue = 10,
            Type = ParameterType.Color,
            MinValue = 0,
            MaxValue = 255,
            DefaultValue = 10,
            Id = 1,
        },
        new ParameterSettings<string>()
        {
            Name = "Text Parameter",
            CurrentValue = "Some text",
            Type = ParameterType.Text,
            DefaultValue = string.Empty,
            Id = 1,
        },
    };

    public MainPage()
    {
        InitializeComponent();
    }
}