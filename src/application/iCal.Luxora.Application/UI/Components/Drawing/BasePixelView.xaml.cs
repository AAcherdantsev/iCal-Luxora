using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using SKPaintSurfaceEventArgs = SkiaSharp.Views.Maui.SKPaintSurfaceEventArgs;

namespace iCal.Luxora.Application.UI.Components.Drawing;

public partial class BasePixelView : ContentView
{
    private const int DEFAULT_WIDTH_PIXELS = 32;
    private const int DEFAULT_HEIGHT_PIXELS = 32;

    private SKColor[] Pixels { get; set; }
    private SKBitmap Bitmap { get; set; }


    public static readonly BindableProperty WidthPixelsProperty = BindableProperty.Create(
        nameof(WidthPixels), typeof(int), typeof(BasePixelView), -1,
        propertyChanged: (b, o, n) => ((BasePixelView)b).OnDimensionsChanged());

    public static readonly BindableProperty HeightPixelsProperty = BindableProperty.Create(
        nameof(HeightPixels), typeof(int), typeof(BasePixelView), -1,
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
        if (WidthPixels == -1 || HeightPixels == -1) return;
        Pixels = new SKColor[WidthPixels * HeightPixels];
        
        for (int i = 0; i < Pixels.Length; i++)
            Pixels[i] = SKColors.Black;

        Bitmap = new SKBitmap(WidthPixels, HeightPixels, SKColorType.Bgra8888, SKAlphaType.Premul);
        ApplyPixelsToBitmap();
    }
    
    void ApplyPixelsToBitmap()
    {
        Bitmap.Pixels = Pixels;
    }

    private void OnCanvasPaint(object? sender, SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        canvas.Clear(SKColors.Transparent);

        var info = e.Info;
        float pageWidth = info.Width;
        float pageHeight = info.Height;

        float scale = Math.Min(pageWidth / WidthPixels, pageHeight / HeightPixels);

        float drawWidth = WidthPixels * scale;
        float drawHeight = HeightPixels * scale;
        float left = (pageWidth - drawWidth) / 2f;
        float top = (pageHeight - drawHeight) / 2f;

        var paint = new SKPaint
        {
            FilterQuality = SKFilterQuality.None,
            IsAntialias = false,
        };

        var dest = new SKRect(left, top, left + drawWidth, top + drawHeight);
        canvas.DrawBitmap(Bitmap, dest, paint);
    }
    
    public void SetPixel(int x, int y, SKColor color)
    {
        if (x < 0 || x >= WidthPixels || y < 0 || y >= HeightPixels) return;
        Pixels[y * WidthPixels + x] = color;
        Bitmap.Pixels = Pixels; 
        Canvas.InvalidateSurface(); 
    }

    public void Clear(SKColor color)
    {
        for (int i = 0; i < Pixels.Length; i++)
            Pixels[i] = color;
        Bitmap.Pixels = Pixels;
        Canvas.InvalidateSurface();
    }


    public event EventHandler<bool> TouchStateChanged;


    private bool _isTouch = false;
    
    
    private void OnCanvasTouch(object? sender, SKTouchEventArgs e)
    {
        if (e.ActionType != SKTouchAction.Pressed && e.ActionType != SKTouchAction.Moved)
        {
            if (_isTouch) TouchStateChanged?.Invoke(this, false);
            _isTouch = false;
            return;
        }

        if (!_isTouch)
        {
            TouchStateChanged?.Invoke(this, true);
            _isTouch = true;
        }
        
        var canvasView = (SKCanvasView)sender!;
        var viewW = canvasView.CanvasSize.Width;
        var viewH = canvasView.CanvasSize.Height;
        float scale = Math.Min(viewW / WidthPixels, viewH / HeightPixels);
        float left = (viewW - WidthPixels * scale) / 2f;
        float top = (viewH - HeightPixels * scale) / 2f;

        float px = (e.Location.X - left) / scale;
        float py = (e.Location.Y - top) / scale;

        int ix = (int)Math.Floor(px);
        int iy = (int)Math.Floor(py);

        if (ix >= 0 && ix < WidthPixels && iy >= 0 && iy < HeightPixels)
        {
            SetPixel(ix, iy, SKColors.DarkRed);
            e.Handled = true;
        }
    }
}