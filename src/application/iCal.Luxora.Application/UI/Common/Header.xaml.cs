namespace iCal.Luxora.Application.UI.Common;

public partial class Header : ContentView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title), typeof(string), typeof(Header), string.Empty);
    
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    
    public Header()
    {
        InitializeComponent();
    }
}