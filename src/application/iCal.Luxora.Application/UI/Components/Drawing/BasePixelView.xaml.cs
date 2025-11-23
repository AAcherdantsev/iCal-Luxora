namespace iCal.Luxora.Application.UI.Components.Drawing;

public partial class BasePixelView : ContentView
{
    private const int DEFAULT_WIDTH_PIXELS = 32;
    private const int DEFAULT_HEIGHT_PIXELS = 32;


    public static readonly BindableProperty WidthPixelsProperty = BindableProperty.Create(
        nameof(WidthPixels), typeof(int), typeof(BasePixelView), 32,
        propertyChanged: (b, o, n) => ((BasePixelView)b).OnDimensionsChanged());

    public static readonly BindableProperty HeightPixelsProperty = BindableProperty.Create(
        nameof(HeightPixels), typeof(int), typeof(BasePixelView), 32,
        propertyChanged: (b, o, n) => ((BasePixelView)b).OnDimensionsChanged());

    public BasePixelView()
    {
        InitializeComponent();
    }

    public int WidthPixels
    {
        get => (int)GetValue(WidthPixelsProperty);
        set => SetValue(WidthPixelsProperty, Math.Max(1, value));
    }

    public int HeightPixels
    {
        get => (int)GetValue(HeightPixelsProperty);
        set => SetValue(HeightPixelsProperty, Math.Max(1, value));
    }

    private void OnDimensionsChanged()
    {
    }
}