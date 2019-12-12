using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using MyAoC2019.Utilities;

namespace MyAoC2019.Solutions.Day08
{
    public class Day8 : DayBase
    {
        protected override string Day => "08";

        protected override string PartOne(string input)
        {
            var layer = GetLayerWithFewestZeros(GetImageLayers(25, 6, input.ConvertInputsToIntegers().ToArray()).ToArray());

            return (Count(layer, num => num == 1) * Count(layer, num => num == 2)).ToString();
        }
        protected override string PartTwo(string input)
        {
            var decodedImage = DecodeImage(25, 6, GetImageLayers(25, 6, input.ConvertInputsToIntegers().ToArray()).ToArray());

            if (!UnitTestMode)
            {
                GenerateBWImage(decodedImage);
            }

            // The code below is not related to the assignment, but it just used to output the sum
            // of all values in the decoded image. This can then be used for unit test purposes for example.
            var valueToReturn = 0;

            for (var i = 0; i < decodedImage.GetLength(0); i++)
            {
                for (var k = 0; k < decodedImage.GetLength(1); k++)
                {
                    valueToReturn += decodedImage[i, k];
                }
            }

            return valueToReturn.ToString();
        }

        private int[,] DecodeImage(int width, int height, int[][,] layers)
        {
            var decodedImage = new int[height, width];

            for (var i = 0; i < height; i++)
            {
                for (var k = 0; k < width; k++)
                {
                    foreach (var layer in layers)
                    {
                        if (layer[i, k] != 2)
                        {
                            decodedImage[i, k] = layer[i, k];
                            break;
                        }
                    }
                }
            }

            return decodedImage;
        }
        private int Count<T>(T[,] layer, Func<T, bool> predicate)
        {
            var count = 0;

            for (var i = 0; i < layer.GetLength(0); i++)
            {
                for (var k = 0; k < layer.GetLength(1); k++)
                {
                    if (predicate.Invoke(layer[i, k]))
                    {
                        count++;
                    }
                }
            }

            return count;
        }
        private int[,] GetLayerWithFewestZeros(int[][,] layers)
        {
            var smallestZeroCount = int.MaxValue;
            int[,]? smallestZeroCountArray = null;

            foreach (var layer in layers)
            {
                var zeroCount = 0;

                for (var i = 0; i < layer.GetLength(0); i++)
                {
                    for (var k = 0; k < layer.GetLength(1); k++)
                    {
                        if (layer[i, k] == 0)
                        {
                            zeroCount++;
                        }
                    }
                }

                if (zeroCount < smallestZeroCount)
                {
                    smallestZeroCount = zeroCount;
                    smallestZeroCountArray = layer;
                }
            }

            return smallestZeroCountArray!;
        }
        private IEnumerable<int[,]> GetImageLayers(int width, int height, int[] imageBits)
        {
            for (var i = 0; i < imageBits.Length; i += width * height)
            {
                yield return GenerateImageLayer(width, height, imageBits[i..(i + width * height)]);
            }
        }
        private int[,] GenerateImageLayer(int width, int height, int[] imageBits)
        {
            var image = new int[height, width];
            var imageBitsCount = 0;

            for (var i = 0; i < height; i++)
            {
                for (var k = 0; k < width; k++)
                {
                    image[i, k] = imageBits[imageBitsCount];
                    imageBitsCount++;
                }
            }

            return image;
        }


        private void GenerateBWImage(int[,] pixelMap)
        {
            Console.WriteLine("Do you want to generate a PNG of the decoded image? Type 'Y' and press enter for yes.");

            if (Console.ReadLine().ToUpper() != "Y")
            {
                return;
            }

            Console.WriteLine("Enter the full (absolute) path where the generated image should be saved to: ");
            var path = Console.ReadLine();

            // We will make the dimensions of the image 10x bigger
            var width = pixelMap.GetLength(1) * 10;
            var height = pixelMap.GetLength(0) * 10;

            // Add a margin of 10 pixels to each side of the image
            var bitmap = new Bitmap(width + 20, height + 20);

            // Draw the black border
            for (var i = 0; i < height; i++)
            {
                for (var k = 0; k < width; k++)
                {
                    if ((i < 10 || i > height - 11) && (k < 10 || i > width - 11))
                    {
                        bitmap.SetPixel(k, i, Color.Black);
                    }
                }
            }

            // Draw the pixelMap
            for (var i = 0; i < height; i++)
            {
                for (var k = 0; k < width; k++)
                {
                    var color = pixelMap[i / 10, k / 10] == 0 ? Color.Black : Color.White;

                    // Add 10, because of the extra space for the boarder on each side
                    bitmap.SetPixel(k + 10, i + 10, color);
                }
            }

            using var fs = new FileStream(Path.Combine(path, "AoC2019_Day8_generated.png"), FileMode.Create);

            bitmap.Save(fs, ImageCodecInfo.GetImageEncoders().First(encoder => encoder.MimeType == "image/png"), null);

            Console.WriteLine("The image generation was successful!");
        }
    }
}
