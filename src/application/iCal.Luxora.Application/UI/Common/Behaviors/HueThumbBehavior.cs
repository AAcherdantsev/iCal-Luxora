namespace iCal.Luxora.Application.UI.Common.Behaviors;

public class HueThumbBehavior : Behavior<Slider>
{
    protected override void OnAttachedTo(Slider slider)
    {
        base.OnAttachedTo(slider);
        slider.ValueChanged += OnValueChanged;
        UpdateThumb(slider.Value, slider);
    }

    protected override void OnDetachingFrom(Slider slider)
    {
        base.OnDetachingFrom(slider);
        slider.ValueChanged -= OnValueChanged;
    }

    private void OnValueChanged(object sender, ValueChangedEventArgs e)
    {
        var slider = (Slider)sender;
        UpdateThumb(e.NewValue, slider);
    }

    private void UpdateThumb(double value, Slider slider)
    {
        double hue = value / slider.Maximum;
        slider.ThumbColor = Color.FromHsla(hue, 1.0, 0.5);
    }
}