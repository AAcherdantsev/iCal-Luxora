namespace iCal.Luxora.Application.UI.Common.Behaviors;

public class IntSliderBehavior : Behavior<Slider>
{
    protected override void OnAttachedTo(Slider bindable)
    {
        base.OnAttachedTo(bindable);
        bindable.ValueChanged += OnSliderValueChanged;
    }

    protected override void OnDetachingFrom(Slider bindable)
    {
        base.OnDetachingFrom(bindable);
        bindable.ValueChanged -= OnSliderValueChanged;
    }

    private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        var slider = (Slider)sender;
        var rounded = (int)Math.Round(e.NewValue);

        if (slider.Value != rounded)
            slider.Value = rounded;
    }
}