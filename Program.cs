using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

if (args.Length < 1)
{
    Console.WriteLine("Usage: SierpinskiTriangle.exe <level>");
    return 1;
}

if (!int.TryParse(args[0], out var level))
{
    Console.WriteLine("Level must be an integer");
    return 1;
}

var peaks = GetSierpinskiTriangleTrianglesPeaks(level);
if (peaks is null)
{
    Console.WriteLine("No implemented this level yet");
    return 1;
}

var maxPeak = peaks.MaxBy(x => x.X);
Image<Rgb24> image = new(maxPeak.X + 2, maxPeak.Y + 2);
image.ProcessPixelRows(accessor =>
{
    Rgb24 primary = Color.Gray;
    var peaksGroupedByY = peaks.GroupBy(peak => peak.Y);
    foreach (var y in peaksGroupedByY)
    {
        var row = accessor.GetRowSpan(y.Key);
        var lowRow = accessor.GetRowSpan(y.Key + 1);
        foreach (var x in y)
        {
            row[x.X] = primary;
            lowRow[x.X - 1] = primary;
            lowRow[x.X] = primary;
            lowRow[x.X + 1] = primary;
        }
    }
});

using var imageFileStream = File.OpenWrite("test.bmp");
image.SaveAsBmp(imageFileStream);

return 0;

// Triangle size is 3x2 (width x height)
// Return null if it's not implemented yet
Point[]? GetSierpinskiTriangleTrianglesPeaks(int level = 0)
{
    if (level == 0)
        return [new Point(1, 0)];
    if (level == 1)
        return
        [
            new Point(3, 0),
            new Point(1, 2),
            new Point(5, 2)
        ];
    if (level == 2)
        return
        [
            new Point(7, 0),
            new Point(5, 2),
            new Point(9, 2),
            new Point(3, 4),
            new Point(11, 4),
            new Point(1, 6),
            new Point(5, 6),
            new Point(9, 6),
            new Point(13, 6)
        ];
    if (level == 3)
        return
        [
            new Point(15, 0),
            new Point(13, 2),
            new Point(17, 2),
            new Point(11, 4),
            new Point(19, 4),
            new Point(9, 6),
            new Point(13, 6),
            new Point(17, 6),
            new Point(21, 6),
            new Point(7, 8),
            new Point(23, 8),
            new Point(5, 10),
            new Point(9, 10),
            new Point(21, 10),
            new Point(25, 10),
            new Point(3, 12),
            new Point(11, 12),
            new Point(19, 12),
            new Point(27, 12),
            new Point(1, 14),
            new Point(5, 14),
            new Point(9, 14),
            new Point(13, 14),
            new Point(17, 14),
            new Point(21, 14),
            new Point(25, 14),
            new Point(29, 14)
        ];
    return null;
}