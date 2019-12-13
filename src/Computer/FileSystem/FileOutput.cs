using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace MyAoC2019.Computer.FileSystem
{
    public static class FileOutput
    {
        public static void GenerateImage(Bitmap bitmap, string filename, string? path = null, string mimeType = "png", long jpegQuality = 95L)
        {
            if (path == null)
            {
                Console.WriteLine("Do you want to generate a PNG of the registration identifier? Type 'Y' and press enter for yes.");

                if (Console.ReadLine().ToUpper() != "Y")
                {
                    return;
                }

                Console.WriteLine("Enter the full (absolute) path where the generated image should be saved to: ");
                path = Console.ReadLine();
            }

            using var fs = new FileStream(Path.Combine(path, $"{filename}.{mimeType}"), FileMode.Create);

            EncoderParameters? parameters = null;

            if (mimeType == "jpg" || mimeType == "jpeg")
            {
                parameters = new EncoderParameters()
                {
                    Param = new EncoderParameter[]
                    {
                        new EncoderParameter(Encoder.Quality, jpegQuality)
                    }
                };
            }

            bitmap.Save(fs, ImageCodecInfo.GetImageEncoders().First(encoder => encoder.MimeType == $"image/{mimeType}"), parameters);

            Console.WriteLine("The image generation was successful!");
        }
    }
}
