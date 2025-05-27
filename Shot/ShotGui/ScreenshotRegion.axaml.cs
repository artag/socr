using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using ImageMagick;
using Tesseract;

namespace ShotGui;

public partial class ScreenshotRegion : Window
{
    private readonly IWindowsMediator _windowsMediator;
    private bool _mouseDownForWindowMoving = false;
    private PointerPoint _originalPoint;

    public ScreenshotRegion(IWindowsMediator windowsMediator)
    {
        InitializeComponent();
        _windowsMediator = windowsMediator;
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var target = this;
        var x = target.Position.X;
        var y = target.Position.Y;
        var width = (uint)target.Width;
        var height = (uint)target.Height;
        var settings = new MagickReadSettings
        {
            ExtractArea = new MagickGeometry(x, y, width, height)
        };

        this.Hide();
        using var mImage = new MagickImage("screenshot:", settings);
        //File.WriteAllBytes($"E:\\Temp\\{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.jpg", data);

        byte[] data = mImage.ToByteArray(MagickFormat.Jpeg);
        using var engine = new TesseractEngine("E:\\Temp\\1", "eng", EngineMode.LstmOnly);

        using var img = Pix.LoadFromMemory(data);
        using var recognizedPage = engine.Process(img);
        var recognizedText = recognizedPage.GetText();

        var topLevel = TopLevel.GetTopLevel(this);
        var task = topLevel!.Clipboard!.SetTextAsync(recognizedText);
        Task.WaitAll(task);

        //_parent.Show();
        _windowsMediator.SwitchToMainWindow();
    }

    private void InputElement_OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (!_mouseDownForWindowMoving)
            return;

        var currentPoint = e.GetCurrentPoint(this);
        Position = new PixelPoint(Position.X + (int)(currentPoint.Position.X - _originalPoint.Position.X),
            Position.Y + (int)(currentPoint.Position.Y - _originalPoint.Position.Y));
    }

    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (WindowState == WindowState.Maximized
            || WindowState == WindowState.FullScreen)
            return;

        _mouseDownForWindowMoving = true;
        _originalPoint = e.GetCurrentPoint(this);
    }

    private void InputElement_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        _mouseDownForWindowMoving = false;
    }
}
