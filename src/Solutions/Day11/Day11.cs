using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using MyAoC2019.Devices.Robots;
using MyAoC2019.Utilities;

namespace MyAoC2019.Solutions.Day11
{
    public class Day11 : DayBase
    {
        protected override string Day => "11";

        protected override string PartOne(string input)
        {
            var robot = new PaintingRobot(input.SplitInputs().ConvertInputsToLongs().ToArray());
            robot.Paint();

            return robot.GetUniquePaintingSteps().ToString();
        }

        protected override string PartTwo(string input)
        {
            var robot = new PaintingRobot(input.SplitInputs().ConvertInputsToLongs().ToArray(), Color.White);
            robot.Paint();

            if (!UnitTestMode)
            {
                GenerateBWImage(robot.DrawBitmap(robot.GetGridDimensions(), 20, new Size(30, 30)), "AoC2019_Day11_generated");
            }

            // Return this to unit tests, but verify the actuall registration identifier in a generated image.
            return robot.GetUniquePaintingSteps().ToString();
        }

        private void GenerateBWImage(Bitmap bitmap, string filename)
        {
            Console.WriteLine("Do you want to generate a PNG of the registration identifier? Type 'Y' and press enter for yes.");

            if (Console.ReadLine().ToUpper() != "Y")
            {
                return;
            }

            Console.WriteLine("Enter the full (absolute) path where the generated image should be saved to: ");
            var path = Console.ReadLine();

            using var fs = new FileStream(Path.Combine(path, $"{filename}.png"), FileMode.Create);

            bitmap.Save(fs, ImageCodecInfo.GetImageEncoders().First(encoder => encoder.MimeType == "image/png"), null);

            Console.WriteLine("The image generation was successful!");
        }
    }
}
