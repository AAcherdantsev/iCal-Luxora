using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using AndroidX.Core.View;
using Color = Android.Graphics.Color;

namespace iCal.Luxora.Application;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        WindowCompat.SetDecorFitsSystemWindows(Window, false);

        // 2) Устанавливаем режим для выреза (notch) — только на Android P+
        if (Build.VERSION.SdkInt >= BuildVersionCodes.P)
        {
            var lp = Window.Attributes;
            lp.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.ShortEdges;
            Window.Attributes = lp;
        }

        // 3) Делаем статусбар прозрачным
        if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
        {
            Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Window.SetStatusBarColor(Color.Transparent);
            Window.SetNavigationBarColor(Color.Transparent);
        }

        // 4) Устанавливаем поведение системных панелей (чтобы они скрывались жестом, если нужно)
        var controller = new WindowInsetsControllerCompat(Window, Window.DecorView);
        controller.SystemBarsBehavior = WindowInsetsControllerCompat.BehaviorShowTransientBarsBySwipe;
    }

    private void MakeStatusBarTransparent()
    {
        if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
        {
            Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            Window.SetStatusBarColor(Color.Transparent);

            Window.DecorView.SystemUiVisibility =
                (StatusBarVisibility)(
                    SystemUiFlags.LayoutFullscreen |
                    SystemUiFlags.LayoutStable
                );
        }
    }
}