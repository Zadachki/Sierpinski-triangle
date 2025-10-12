using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

Image<Rgb24> image = new(32, 32);

using var imageFileStream = File.OpenWrite("test.bmp");
image.SaveAsBmp(imageFileStream);