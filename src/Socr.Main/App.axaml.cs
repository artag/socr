using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input.Platform;
using Avalonia.Markup.Xaml;
using CSharpFunctionalExtensions;
using Socr.Domain;
using Tesseract;

namespace Socr.Main;

internal sealed partial class App : Application
{
    /// <inheritdoc />
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    /// <inheritdoc />
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var screenshot = new ScreenshotFromScreen();
            var ocr = new Ocr();

            var mainWindow = new MainWindow();
            var topLevel = TopLevel.GetTopLevel(mainWindow);
            var clipboard = new Clipboard(topLevel!.Clipboard!);
            var display = new DisplayAdapter(mainWindow);
            var mainScenario = new MainScenario(
                screenshot.Make,
                ocr.RecognizeText,
                clipboard.SetRecognizedText,
                display.DisplayMessage);

            mainWindow.MainScenario = mainScenario;
            desktop.MainWindow = mainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }
}

internal class Clipboard
{
    private readonly IClipboard _clipboard;

    public Clipboard(IClipboard clipboard)
    {
        _clipboard = clipboard ?? throw new ArgumentNullException(nameof(clipboard));
    }

    public Task<Result> SetRecognizedText(RecognizedText recognizedText)
    {
        return Result.Try(
        async () =>
        {
            await _clipboard
                .SetTextAsync(recognizedText.Value)
                .ConfigureAwait(false);
        });
    }
}

internal class DisplayAdapter
{
    private readonly MainWindow _mainWindow;

    public DisplayAdapter(MainWindow mainWindow)
    {
        _mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
    }

    public Result DisplayMessage(Message message)
    {
        return Result.Try(
        () =>
        {
            _mainWindow.SetStatus(message.Value);
        });
    }
}
