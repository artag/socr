using CSharpFunctionalExtensions;
using ImageMagick;
using Socr.Domain;

namespace Socr.Main;

internal sealed class ScreenshotFromScreen
{
    public Task<Result<Screenshot>> Make(ScreenRegion screenRegion)
    {
        var x = screenRegion.X;
        var y = screenRegion.Y;
        var width = screenRegion.Width;
        var height = screenRegion.Height;
        var settings = new MagickReadSettings
        {
            ExtractArea = new MagickGeometry(x, y, width, height),
        };

        var result = Result.Try(
        () =>
        {
            using var mImage = new MagickImage("screenshot:", settings);
            var data = mImage.ToByteArray(MagickFormat.Png);
            var screenshot = new Screenshot(data);
            return screenshot;
        },
        ex =>
            $"Error on get sreenshot. {ex.Message}");

        return Task.FromResult(result);
    }
}
