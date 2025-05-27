using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SharpHook;
using SharpHook.Data;

namespace ShotGui;

public partial class App : Application
{
    private WindowsMediator _windowsMediator;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            InitializeApp(desktop);
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void InitializeApp(IClassicDesktopStyleApplicationLifetime desktop)
    {
        _windowsMediator = new WindowsMediator();

        var mainWindow = new MainWindow(_windowsMediator);
        desktop.MainWindow = mainWindow;

        var sr = new ScreenshotRegion(_windowsMediator);
        _windowsMediator.MainWindow = mainWindow;
        _windowsMediator.ScreenshotRegion = sr;

        //var hook = new TaskPoolGlobalHook();
        //hook.KeyTyped += OnKeyTyped;
        //hook.RunAsync();
    }

    private void OnKeyTyped(object? sender, KeyboardHookEventArgs e)
    {
        if (e.RawEvent.Mask == EventMask.LeftAlt
            && e.RawEvent.Keyboard.KeyChar == 's')
        {
            _windowsMediator.SwitchToScreenshotRegion();
        }

        return;
    }
}
