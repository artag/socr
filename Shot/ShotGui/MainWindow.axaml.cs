using System.Threading;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace ShotGui
{
    public partial class MainWindow : Window
    {
        //private Window _screenshotRegion;
        //private readonly SelectedRegion _selectedRegion;
        private readonly IWindowsMediator _windowsMediator;

        public MainWindow(IWindowsMediator windowsMediator)
        {
            InitializeComponent();
            _windowsMediator = windowsMediator;
        }

        //private Window ScreenshotRegion =>
        //    LazyInitializer.EnsureInitialized(
        //        ref _screenshotRegion,
        //        CreateScreenshotRegion);

        private void Button_OnClick(object? sender, RoutedEventArgs e)
        {
            //this.Hide();
            //ScreenshotRegion.Show();
            _windowsMediator.SwitchToScreenshotRegion();
        }

        //private Window CreateScreenshotRegion()
        //{
        //    var r = new ScreenshotRegion(this);
        //    //r.Hide();
        //    return r;
        //}

        //protected override void OnClosing(WindowClosingEventArgs e)
        //{
        //    ScreenshotRegion.Close();
        //    base.OnClosing(e);
        //}
    }
}
