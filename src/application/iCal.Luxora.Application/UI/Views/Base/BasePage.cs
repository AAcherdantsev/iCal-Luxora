namespace iCal.Luxora.Application.UI.Views.Base;

[ContentProperty(nameof(Content))]
public class BasePage : ContentPage
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title), typeof(string), typeof(BasePage), string.Empty);

    public BasePage()
    {
        InitBaseUI();
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    private void InitBaseUI()
    {
        
    }
}