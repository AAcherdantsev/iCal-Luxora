namespace iCal.Luxora.Application.UI.Views;

public partial class MainPage : ContentPage
{
    private int count;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object? sender, EventArgs e)
    {
        count++;
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        //HeaderStack.WidthRequest = this.Width;
    }
}