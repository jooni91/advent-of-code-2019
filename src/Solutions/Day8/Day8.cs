using System;
using System.Collections.Generic;
using System.Linq;
using MyAoC2019.Utilities;

namespace MyAoC2019.Solutions.Day8
{
    public class Day8 : DayBase
    {
        protected override int Day => 8;

        protected override string PartOne(string input)
        {
            var layer = GetLayerWithFewestZeros(GetImageLayers(25, 6, input.ConvertInputsToIntegers().ToArray()).ToArray());

            return (Count(layer, num => num == 1) * Count(layer, num => num == 2)).ToString();
        }
        protected override string PartTwo(string input)
        {
            var decodedImage = DecodeImage(25, 6, GetImageLayers(25, 6, input.ConvertInputsToIntegers().ToArray()).ToArray());



            // The code below is not related to the assignment, but it just used to output the sum
            // of all values in the decoded image. This can then be used for unit test purposes for example.
            var valueToReturn = 0;

            for (int i = 0; i < decodedImage.GetLength(0); i++)
            {
                for (int k = 0; k < decodedImage.GetLength(1); k++)
                {
                    valueToReturn += decodedImage[i, k];
                }
            }

            return valueToReturn.ToString();
        }

        private int[,] DecodeImage(int width, int height, int[][,] layers)
        {
            var decodedImage = new int[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int k = 0; k < width; k++)
                {
                    foreach(var layer in layers)
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
            int count = 0;

            for (int i = 0; i < layer.GetLength(0); i++)
            {
                for (int k = 0; k < layer.GetLength(1); k++)
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
            int smallestZeroCount = int.MaxValue;
            int[,]? smallestZeroCountArray = null;

            foreach(var layer in layers)
            {
                int zeroCount = 0;

                for (int i = 0; i < layer.GetLength(0); i++)
                {
                    for (int k = 0; k < layer.GetLength(1); k++)
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
            for (int i = 0; i < imageBits.Length; i += width * height)
            {
                yield return GenerateImageLayer(width, height, imageBits[i..(i + (width * height))]);
            }
        }
        private int[,] GenerateImageLayer(int width, int height, int[] imageBits)
        {
            var image = new int[height, width];
            int imageBitsCount = 0;

            for (int i = 0; i < height; i++)
            {
                for (int k = 0; k < width; k++)
                {
                    image[i, k] = imageBits[imageBitsCount];
                    imageBitsCount++;
                }
            }

            return image;
        }
    }
}
