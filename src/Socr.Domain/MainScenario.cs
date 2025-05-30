using CSharpFunctionalExtensions;

namespace Socr.Domain;

public record ScreenRegion(
    int X,
    int Y,
    uint Width,
    uint Height
);

public record Screenshot(
    byte[] Data
);

public record RecognizedText(
    string Value
);

public record Message(
    string Value
);

public delegate Task<Result<Screenshot>> MakeScreenShot(ScreenRegion screenRegion);

public delegate Task<Result<RecognizedText>> RecognizeText(Screenshot screenShot);

public delegate Task<Result> SetRecognizedTextToClipboard(RecognizedText recognizedText);

public delegate Result DisplayMessage(Message message);

public class MainScenario
{
    private readonly MakeScreenShot _makeScreenShot;
    private readonly RecognizeText _recognizeText;
    private readonly SetRecognizedTextToClipboard _setTextToClipboard;
    private readonly DisplayMessage _displayMessage;

    public MainScenario(
        MakeScreenShot makeScreenShot,
        RecognizeText recognizeText,
        SetRecognizedTextToClipboard setTextToClipboard,
        DisplayMessage displayMessage)
    {
        _makeScreenShot = makeScreenShot;
        _recognizeText = recognizeText;
        _setTextToClipboard = setTextToClipboard;
        _displayMessage = displayMessage;
    }

    public async Task Execute(ScreenRegion screenRegion)
    {
        var ocrResult = await _makeScreenShot
            .Invoke(screenRegion)
            .Bind(s => _recognizeText.Invoke(s))
            .Bind(t => _setTextToClipboard.Invoke(t))
            .ConfigureAwait(false);

        ocrResult
            .Tap(() => _displayMessage(new Message("Success OCR")))
            .TapError(err => _displayMessage(new Message(err)));
    }
}
