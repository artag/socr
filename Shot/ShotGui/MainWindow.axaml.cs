using System.Threading;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace ShotGui
{
    public partial class MainWindow : Window
    {
        private Window _screenshotRegion;
        private readonly SelectedRegion _selectedRegion;

        public MainWindow()
        {
            InitializeComponent();
            _selectedRegion = new SelectedRegion();
        }

        private Window ScreenshotRegion =>
            LazyInitializer.EnsureInitialized(
                ref _screenshotRegion,
                CreateScreenshotRegion);

        private void Button_OnClick(object? sender, RoutedEventArgs e)
        {
            ScreenshotRegion.Show(this);
            var i = 42;
        }

        private Window CreateScreenshotRegion()
        {
            return new ScreenshotRegion();
        }
    }
}
