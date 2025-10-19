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
    DrawTriforse(image, new PointF(200f, 200f), 50f, Color.Tan);

    image.SaveAsPng("serpinski triangle.png");
} // Dispose - releasing memory into a memory pool ready for the next image you wish to process.

void DrawTriforse(Image<Rgba32> image, PointF tcPoint, float sideLength, Color color)
{
    var triangleHeight = float.Sqrt(3) * sideLength / 2f;

    var topTriangleBlPoint = new PointF(tcPoint.X - sideLength / 2f, tcPoint.Y + triangleHeight);

    DrawTriangle(image, topTriangleBlPoint, sideLength, color);

    var leftTriangleBlPoint = new PointF(topTriangleBlPoint.X - sideLength / 2f, topTriangleBlPoint.Y + triangleHeight);

    DrawTriangle(image, leftTriangleBlPoint, sideLength, color);

    var rightTriangleBlPoint = new PointF(tcPoint.X, leftTriangleBlPoint.Y);

    DrawTriangle(image, rightTriangleBlPoint, sideLength, color);
}

void DrawTriangle(Image<Rgba32> image, PointF blPoint, float sideLength, Color color)
{
    float height = float.Sqrt(3f) * sideLength / 2f;

    PointF[] points = new PointF[] {
        blPoint,
        new PointF(blPoint.X + sideLength, blPoint.Y),
        new PointF(blPoint.X + sideLength / 2.0f, blPoint.Y - height)
    };

    Path path = new Path(points);

    SolidPen pen = Pens.Solid(color, 1);

    image.Mutate(img => img
        .Fill(color, path));
}