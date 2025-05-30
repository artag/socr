using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CSharpFunctionalExtensions;
using Socr.Domain;

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

            var mainScenario = new MainScenario(
                screenshot.Make,
                ocr.RecognizeText,
                SetRecognizedTextToClipboard,
                DisplayMessage);

            desktop.MainWindow = new MainWindow()
            {
                MainScenario = mainScenario
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private Task<Result> SetRecognizedTextToClipboard(RecognizedText recognizedText)
    {
        throw new NotImplementedException();
    }

    private Result DisplayMessage(Message message)
    {
        throw new NotImplementedException();
    }
}
