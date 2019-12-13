using System.Drawing;
using System.Linq;
using MyAoC2019.Devices.Robots;
using MyAoC2019.Utilities;
using Xunit;

namespace MyAoC2019.Tests.Devices.Robots
{
    public class PaintingRobotTests
    {
        [Fact]
        public void Paint_Should_Paint()
        {
            // Arrange
            var input = InputLoader.LoadInputsFromFileAsString("InputFiles/PaintingRobotTestFile.txt");
            var robot = new PaintingRobot(input.SplitInputs().ConvertInputsToLongs().ToArray(), Color.White);

            // Act
            robot.Paint();

            // Assert
            Assert.True(robot.GetUniquePaintingSteps() > 0);
        }

        [Fact]
        public void GetUniquePaintingSteps_ShouldReturn_ExpectedCount()
        {
            // Arrange
            var input = InputLoader.LoadInputsFromFileAsString("InputFiles/PaintingRobotTestFile.txt");
            var robot = new PaintingRobot(input.SplitInputs().ConvertInputsToLongs().ToArray());

            // Act
            robot.Paint();

            // Assert
            Assert.Equal(2016, robot.GetUniquePaintingSteps());
        }
    }
}
