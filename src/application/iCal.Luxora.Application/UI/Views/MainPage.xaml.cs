using iCal.Luxora.Models.Abstractions;
using iCal.Luxora.Models.Enums;
using iCal.Luxora.Models.Parameters;

namespace iCal.Luxora.Application.UI.Views;

public partial class MainPage : ContentPage
{
    public IParameterSettings ParameterSettings { get; set; } = new ParameterSettings<int>()
    {
        Name = "Some Parameter",
        CurrentValue = 3,
        Type = ParameterType.Slider,
        DefaultValue = 50,
        MinValue = 0,
        MaxValue = 100,
        Id = 1,
    };

    public MainPage()
    {
        InitializeComponent();
    }
}