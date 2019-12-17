using System.Linq;
using MyAoC2019.Computer.FileSystem;
using MyAoC2019.Computer.GPU;
using MyAoC2019.Devices.Robots;

namespace MyAoC2019.Solutions.Day15
{
    public class Day15 : DayBase
    {
        protected override string Day => "15";

        protected override string PartOne(string input)
        {
            var robot = new RepairDroid(input);
            var map = robot.ExploreTheMap();

            if (!UnitTestMode && true)
            {
                var drawableMap = robot.ConvertToDrawableGrid();
                using var bitmap = GraphicsOutput.DrawBitmap(drawableMap, GraphicsOutput.GetDimensions(drawableMap), 10f);
                FileOutput.GenerateImage(bitmap, "AoC2019_Day15_generated", @"../../../../images");
            }

            return (map.Count(tile => tile.Value == TileType.Empty) + 2).ToString();
        }

        protected override string PartTwo(string input)
        {
            var robot = new RepairDroid(input);
            var map = robot.ExploreTheMap();



            return "";
        }
    }
}
