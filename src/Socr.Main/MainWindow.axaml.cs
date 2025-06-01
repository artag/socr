using Avalonia.Controls;
using Avalonia.Interactivity;
using Socr.Domain;

namespace Socr.Main;

public partial class MainWindow : Window
{
    private ScreenRegionWindow _screenRegion;

    /// <summary>
    /// Constructor.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
    }

    public MainScenario MainScenario { get; set; }

    public void SetStatus(string text)
    {
        Status.Text = text;
    }

    private ScreenRegionWindow ScreenRegionWindow =>
        LazyInitializer.EnsureInitialized(
            ref _screenRegion,
            CreateScreenshotRegion);

    protected override void OnClosing(WindowClosingEventArgs e)
    {
        ScreenRegionWindow.Close();
        base.OnClosing(e);
    }

    private void CaptureButton_OnClick(object? sender, RoutedEventArgs e)
    {
        ScreenRegionWindow.Show();
    }

    private ScreenRegionWindow CreateScreenshotRegion()
    {
        var r = new ScreenRegionWindow()
        {
            MainScenario = MainScenario
        };

        return r;
    }
}
