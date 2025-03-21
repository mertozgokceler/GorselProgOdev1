using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;

namespace HesMak.WinUI;


public partial class App : MauiWinUIApplication
{
    public App()
    {
        this.InitializeComponent();
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

       
        var window = Application.Windows[0].Handler.PlatformView as MauiWinUIWindow;
        if (window is not null)
        {
           
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
            var windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
            var appWindow = AppWindow.GetFromWindowId(windowId);

            if (appWindow is not null)
            {
                appWindow.Resize(new SizeInt32(470, 670));
            }
        }
    }
}
