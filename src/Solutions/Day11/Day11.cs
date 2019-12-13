using System.Drawing;
using System.Linq;
using MyAoC2019.Computer.FileSystem;
using MyAoC2019.Computer.GPU;
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
                using var bitmap = GraphicsOutput.DrawBitmap(robot.Grid, GraphicsOutput.GetDimensions(robot.Grid), 20, robot.BackgroundDefaultColor, new Size(30, 30), Color.Black);
                FileOutput.GenerateImage(bitmap, "AoC2019_Day11_generated");
            }

            // Return this to unit tests, but verify the actuall registration identifier in a generated image.
            return robot.GetUniquePaintingSteps().ToString();
        }
    }
}
