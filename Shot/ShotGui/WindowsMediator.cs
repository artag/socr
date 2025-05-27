using Avalonia.Controls;

namespace ShotGui;

internal class WindowsMediator : IWindowsMediator
{
    public Window MainWindow { get; set; }

    public Window ScreenshotRegion { get; set; }

    public void SwitchToMainWindow()
    {
        ScreenshotRegion.Hide();
        MainWindow.Show();
    }

    public void SwitchToScreenshotRegion()
    {
        MainWindow.Hide();
        ScreenshotRegion.Show();
    }
}
