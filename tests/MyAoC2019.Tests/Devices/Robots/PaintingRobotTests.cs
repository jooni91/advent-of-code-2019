using System;
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

        [Fact]
        public void DrawBitmap_ShouldReturn_Bitmap()
        {
            // Arrange
            var input = InputLoader.LoadInputsFromFileAsString("InputFiles/PaintingRobotTestFile.txt");
            var robot = new PaintingRobot(input.SplitInputs().ConvertInputsToLongs().ToArray(), Color.White);

            // Act
            robot.Paint();
            var bitmap = robot.DrawBitmap(new Size(10, 10));

            // Assert
            Assert.NotNull(bitmap);
        }

        [Fact]
        public void DrawBitmap_ShouldThrow_OnZeroWidth()
        {
            // Arrange
            var input = InputLoader.LoadInputsFromFileAsString("InputFiles/PaintingRobotTestFile.txt");
            var robot = new PaintingRobot(input.SplitInputs().ConvertInputsToLongs().ToArray());

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => robot.DrawBitmap(new Size(0, 10)));
        }

        [Fact]
        public void DrawBitmap_ShouldThrow_OnZeroHeight()
        {
            // Arrange
            var input = InputLoader.LoadInputsFromFileAsString("InputFiles/PaintingRobotTestFile.txt");
            var robot = new PaintingRobot(input.SplitInputs().ConvertInputsToLongs().ToArray());

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => robot.DrawBitmap(new Size(10, 0)));
        }

        [Fact]
        public void DrawBitmap_Should_IncludePadding()
        {
            // Arrange
            var input = InputLoader.LoadInputsFromFileAsString("InputFiles/PaintingRobotTestFile.txt");
            var robot = new PaintingRobot(input.SplitInputs().ConvertInputsToLongs().ToArray(), Color.White);

            // Act
            robot.Paint();
            var bitmap = robot.DrawBitmap(new Size(10, 10), 1f, new Size(10, 10));

            // Assert
            Assert.Equal(30, bitmap.Width);
            Assert.Equal(30, bitmap.Height);
        }

        [Fact]
        public void DrawBitmap_Should_ScaleSize()
        {
            // Arrange
            var input = InputLoader.LoadInputsFromFileAsString("InputFiles/PaintingRobotTestFile.txt");
            var robot = new PaintingRobot(input.SplitInputs().ConvertInputsToLongs().ToArray(), Color.White);

            // Act
            robot.Paint();
            var bitmap = robot.DrawBitmap(new Size(10, 10), 2f);

            // Assert
            Assert.Equal(20, bitmap.Width);
            Assert.Equal(20, bitmap.Height);
        }

        [Fact]
        public void DrawBitmap_ShouldNot_ScalePadding()
        {
            // Arrange
            var input = InputLoader.LoadInputsFromFileAsString("InputFiles/PaintingRobotTestFile.txt");
            var robot = new PaintingRobot(input.SplitInputs().ConvertInputsToLongs().ToArray(), Color.White);

            // Act
            robot.Paint();
            var bitmap = robot.DrawBitmap(new Size(10, 10), 2f, new Size(10, 10));

            // Assert
            Assert.Equal(40, bitmap.Width);
            Assert.Equal(40, bitmap.Height);
        }

        [Fact]
        public void DrawBitmap_Should_DrawBorder()
        {
            // Arrange
            var input = InputLoader.LoadInputsFromFileAsString("InputFiles/PaintingRobotTestFile.txt");
            var robot = new PaintingRobot(input.SplitInputs().ConvertInputsToLongs().ToArray(), Color.White);

            // Act
            robot.Paint();
            var bitmap = robot.DrawBitmap(new Size(10, 10), 1f, new Size(10, 10), Color.Red);

            // Assert
            Assert.Equal(Color.Red.R, bitmap.GetPixel(0, 0).R);
            Assert.Equal(Color.Red.R, bitmap.GetPixel(bitmap.Width - 1, 0).R);
            Assert.Equal(Color.Red.R, bitmap.GetPixel(0, bitmap.Height - 1).R);
            Assert.Equal(Color.Red.R, bitmap.GetPixel(bitmap.Width - 1, bitmap.Height - 1).R);
        }

        [Fact]
        public void GetGridDimensions_ShouldReturn_ExpectedDimensions()
        {
            // Arrange
            var input = InputLoader.LoadInputsFromFileAsString("InputFiles/PaintingRobotTestFile.txt");
            var robot = new PaintingRobot(input.SplitInputs().ConvertInputsToLongs().ToArray(), Color.White);

            // Act
            robot.Paint();

            // Assert
            Assert.Equal(new Size(42, 5), robot.GetGridDimensions());
        }

        [Fact]
        public void GetGridDimensions_ThrowsInvalidOperationException_WhenGridToSmall()
        {
            // Arrange
            var input = InputLoader.LoadInputsFromFileAsString("InputFiles/PaintingRobotTestFile.txt");
            var robot = new PaintingRobot(input.SplitInputs().ConvertInputsToLongs().ToArray(), Color.White);

            // Act

            // Assert
            Assert.Throws<InvalidOperationException>(() => robot.GetGridDimensions());
        }
    }
}
