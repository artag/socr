using CSharpFunctionalExtensions;
using Socr.Domain;
using Tesseract;

namespace Socr.Main;

internal sealed class Ocr
{
    public Task<Result<RecognizedText>> RecognizeText(Screenshot screenshot)
    {
        using var engine = new TesseractEngine("E:\\Temp\\1", "eng", EngineMode.LstmOnly);
        using var img = Pix.LoadFromMemory(screenshot.Data);
        using var recognizedPage = engine.Process(img);
        var text = recognizedPage.GetText();
        var recognizedText = new RecognizedText(text);
        return Task.FromResult(Result.Success(recognizedText));
    }
}
