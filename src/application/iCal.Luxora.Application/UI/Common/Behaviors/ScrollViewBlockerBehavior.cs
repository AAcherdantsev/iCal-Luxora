using iCal.Luxora.Application.UI.Components.Drawing;
#if IOS
using UIKit;
#elif ANDROID
using Android.Views;
#endif

namespace iCal.Luxora.Application.UI.Common.Behaviors;

public class ScrollViewBlockerBehavior : Behavior<BasePixelView>
{
    private ScrollView? _parentScrollView;
    
    protected override void OnAttachedTo(BasePixelView bindable)
    {
        base.OnAttachedTo(bindable);
        bindable.TouchStateChanged += TouchStateChanged;
        
        var parent = bindable.Parent;
        while (parent != null && parent is not ScrollView)
        {
            parent = parent.Parent;
        }

        if (parent is ScrollView scrollView)
        {
            _parentScrollView = scrollView;
        }
    }
    
    protected override void OnDetachingFrom(BasePixelView bindable)
    {
        base.OnDetachingFrom(bindable);
        bindable.TouchStateChanged -= TouchStateChanged;

        if (_parentScrollView != null)
        {
#if IOS
            if (_parentScrollView.Handler?.PlatformView is UIScrollView uiScrollView)
            {
                uiScrollView.ScrollEnabled = true;
            }
#elif ANDROID
            if (bindable.Handler?.PlatformView is Android.Views.View nativeView)
            {
                nativeView.Parent?.RequestDisallowInterceptTouchEvent(false);
            }
#endif
        }
        _parentScrollView = null;
    }
    
    private void TouchStateChanged(object? sender, bool isTouched)
    {
#if IOS
        if (_parentScrollView?.Handler?.PlatformView is UIScrollView uiScrollView)
        {
            uiScrollView.ScrollEnabled = !isTouched;
        }
#elif ANDROID
        if (sender is BasePixelView view && view.Handler?.PlatformView is Android.Views.View nativeView)
        {
            nativeView.Parent?.RequestDisallowInterceptTouchEvent(isTouched);
        }
#endif
    }
}