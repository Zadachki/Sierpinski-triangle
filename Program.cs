using System.Runtime.CompilerServices;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Path = SixLabors.ImageSharp.Drawing.Path;
using PointF = SixLabors.ImageSharp.PointF;

int width = 640;
int height = 480;

DrawingOptions drawingOptions = new DrawingOptions();

// Creates a new image with empty pixel data. 
using (Image<Rgba32> image = new(width, height))
{
    DrawTriangle(image, new PointF(200f, 200.0f), 50f, Color.PaleVioletRed, false);
    DrawTriangle(image, new PointF(400f, 200.0f), 50f, Color.Tan, true);

    image.SaveAsPng("serpinski triangle.png");
} // Dispose - releasing memory into a memory pool ready for the next image you wish to process.

void DrawTriangle(Image<Rgba32> image, PointF blPoint, float sideLength, Color color, bool fill)
{
    float height = float.Sqrt(3) * sideLength / 2;

    PointF[] points = new PointF[] {
        blPoint,
        new PointF(blPoint.X + sideLength, blPoint.Y),
        new PointF(blPoint.X + sideLength / 2.0f, blPoint.Y - height),
        blPoint
    };

    Path path = new Path(points);

    SolidPen pen = Pens.Solid(color, 1);

    image.Mutate(img => img
        .Draw(drawingOptions, pen, path));

    if (fill)
        image.Mutate(img => img.Fill(color, path));
}