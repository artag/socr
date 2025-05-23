using System;
using System.IO;
using System.Runtime.Intrinsics.Arm;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using ImageMagick;
using SkiaSharp;
//using Avalonia.Media.Imaging;

namespace ShotGui;

public partial class ScreenshotRegion : Window
{
    private bool _mouseDownForWindowMoving = false;
    private PointerPoint _originalPoint;

    public ScreenshotRegion()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var target = this;
        var width = (uint)target.Width;
        var height = (uint)target.Height;
        var settings = new MagickReadSettings
        {
            ExtractArea = new MagickGeometry(20, 30, width, height)
        };

        using (MagickImage mImage = new MagickImage("screenshot:", settings))
        {
            byte[] data = mImage.ToByteArray(MagickFormat.Jpeg);
            File.WriteAllBytes($"E:\\Temp\\{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.jpg", data);
        }

        // Not worked var 3
        //var frame = this.FrameSize;
        //var width = (int)frame.Value.Width;
        //var height = (int)frame.Value.Height;
        //using (var surface = SKSurface.Create(new SKImageInfo(width, height, SKColorType.Rgba8888, SKAlphaType.Premul)))
        //{
        //    // Get the canvas of the surface
        //    var canvas = surface.Canvas;

        //    // Draw the control onto the canvas
        //    control.DrawToBitmap(new System.Drawing.Bitmap(width, height)).GetHbitmap();

        //    // Capture the snapshot of the surface
        //    using (var image = surface.Snapshot())
        //    {
        //        // Save the image to a file
        //        using (var stream = System.IO.File.OpenWrite("e:\\Temp\\w.png"))
        //        {
        //            image.Save(stream, SKEncodedImageFormat.Png, 100);
        //        }
        //    }

        // var 1
        //var target = this;
        //using var surface = SKSurface.Create(new SKImageInfo((int)target.Width, (int)target.Height, SKColorType.Rgba8888, SKAlphaType.Premul));
        //using var image = surface.Snapshot();
        //using var data = image.Encode(SKEncodedImageFormat.Png, 80);
        //using var stream = File.OpenWrite("E:\\Temp\\w.png");
        //data.SaveTo(stream);

        // var 2
        //var target = this;
        //var pixelSize = new PixelSize((int)target.Width, (int)target.Height);
        //var size = new Size(target.Width, target.Height);
        //var dpiVector = new Vector(96, 96);

        //using (RenderTargetBitmap bitmap = new RenderTargetBitmap(pixelSize, dpiVector))
        //{
        //    target.Measure(size);
        //    target.Arrange(new Rect(size));
        //    bitmap.Render(target);
        //    bitmap.Save("E:\\Temp\\w.png");
        //}

        this.Hide();
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
