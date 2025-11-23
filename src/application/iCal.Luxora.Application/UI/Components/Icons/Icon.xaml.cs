namespace iCal.Luxora.Application.UI.Components.Icons;

public partial class Icon : ContentView
{
    public static readonly new BindableProperty BackgroundColorProperty = BindableProperty.Create(
        nameof(BackgroundColor), typeof(Color), typeof(Icon), (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]);
    
    public static readonly BindableProperty IconColorProperty = BindableProperty.Create(
        nameof(IconColor), typeof(Color), typeof(Icon), (Color)Microsoft.Maui.Controls.Application.Current.Resources["SecondaryDark"]);

    public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create(
        nameof(StrokeColor), typeof(Color), typeof(Icon), (Color)Microsoft.Maui.Controls.Application.Current.Resources["PrimaryDark"]);
    
    public static readonly BindableProperty SourceProperty = BindableProperty.Create(
        nameof(Source), typeof(string), typeof(Icon), string.Empty);
    
    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }
    
    public Color IconColor
    {
        get => (Color)GetValue(IconColorProperty);
        set => SetValue(IconColorProperty, value);
    }
    
    public Color StrokeColor
    {
        get => (Color)GetValue(StrokeColorProperty);
        set => SetValue(StrokeColorProperty, value);
    }
    
    public string Source
    {
        get => (string)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }
    
    public Icon()
    {
        InitializeComponent();
    }
}