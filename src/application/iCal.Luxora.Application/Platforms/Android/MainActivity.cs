using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace iCal.Luxora.Application;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        
        /*
        Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
        Window.SetNavigationBarColor(Android.Graphics.Color.Transparent);
        Window.DecorView.SystemUiVisibility =
            (StatusBarVisibility) (SystemUiFlags.LayoutStable | SystemUiFlags.LayoutFullscreen);
*/
        MakeStatusBarTransparent();
    }

    void MakeStatusBarTransparent()
    {
        if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
        {
            Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            Window.SetStatusBarColor(Android.Graphics.Color.Transparent);

            Window.DecorView.SystemUiVisibility =
                (StatusBarVisibility)(
                    SystemUiFlags.LayoutFullscreen |
                    SystemUiFlags.LayoutStable
                );
        }
    }
}