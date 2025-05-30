using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Socr.Domain;

namespace Socr.Main;

public partial class ScreenRegionWindow : Window
{
    private bool _mouseDownForWindowMoving;
    private PointerPoint _originalPoint;

    public ScreenRegionWindow()
    {
        InitializeComponent();
    }

    public MainScenario MainScenario { get; init; }

    private async void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var sr = GetScreenRegion();
        await MainScenario.Execute(sr).ConfigureAwait(false);
    }

    private ScreenRegion GetScreenRegion()
    {
        var x = Position.X;
        var y = Position.Y;
        var width = (uint)Width;
        var height = (uint)Height;
        return new ScreenRegion(x, y, width, height);
    }

    private void ScreenRegion_OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (!_mouseDownForWindowMoving)
            return;

        var currentPoint = e.GetCurrentPoint(this);
        Position = new PixelPoint(
            Position.X + (int)(currentPoint.Position.X - _originalPoint.Position.X),
            Position.Y + (int)(currentPoint.Position.Y - _originalPoint.Position.Y));
    }

    private void ScreenRegion_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (WindowState == WindowState.Maximized
            || WindowState == WindowState.FullScreen)
            return;

        _mouseDownForWindowMoving = true;
        _originalPoint = e.GetCurrentPoint(this);
    }

    private void ScreenRegion_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        _mouseDownForWindowMoving = false;
    }
}
