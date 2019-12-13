using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using MyAoC2019.Computer.GPU;
using MyAoC2019.Devices.Robots;
using MyAoC2019.Utilities;
using Xunit;

namespace MyAoC2019.Tests.Computer.GPU
{
    public class GraphicsOutputTests
    {
        [Fact]
        public void GetGridDimensions_ShouldReturn_ExpectedDimensions()
        {
            // Arrange
            var points = new Dictionary<Vector2, int>
            {
                { new Vector2(5, 2), 5 },
                { new Vector2(2, 2), 5 },
                { new Vector2(1, 8), 5 },
            };

            // Act
            var dimensions = GraphicsOutput.GetDimensions(points);

            // Assert
            Assert.Equal(new Size(5, 7), dimensions);
        }

        [Fact]
        public void GetGridDimensions_ThrowsInvalidOperationException_WhenGridToSmall()
        {
            // Arrange
            var points = new Dictionary<Vector2, int>
            {
                { new Vector2(5, 2), 5 }
            };

            // Act

            // Assert
            Assert.Throws<InvalidOperationException>(() => GraphicsOutput.GetDimensions(points));
        }

        [Fact]
        public void DrawBitmap_ShouldReturn_Bitmap()
        {
            // Arrange
            var input = InputLoader.LoadInputsFromFileAsString("InputFiles/PaintingRobotTestFile.txt");
            var robot = new PaintingRobot(input.SplitInputs().ConvertInputsToLongs().ToArray(), Color.White);

            // Act
            robot.Paint();
            var bitmap = GraphicsOutput.DrawBitmap(robot.Grid, new Size(10, 10));

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
            Assert.Throws<ArgumentOutOfRangeException>(() => GraphicsOutput.DrawBitmap(robot.Grid, new Size(0, 10)));
        }

        [Fact]
        public void DrawBitmap_ShouldThrow_OnZeroHeight()
        {
            // Arrange
            var input = InputLoader.LoadInputsFromFileAsString("InputFiles/PaintingRobotTestFile.txt");
            var robot = new PaintingRobot(input.SplitInputs().ConvertInputsToLongs().ToArray());

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => GraphicsOutput.DrawBitmap(robot.Grid, new Size(10, 0)));
        }

        [Fact]
        public void DrawBitmap_Should_IncludePadding()
        {
            // Arrange
            var input = InputLoader.LoadInputsFromFileAsString("InputFiles/PaintingRobotTestFile.txt");
            var robot = new PaintingRobot(input.SplitInputs().ConvertInputsToLongs().ToArray(), Color.White);

            // Act
            robot.Paint();
            var bitmap = GraphicsOutput.DrawBitmap(robot.Grid, new Size(10, 10), 1f, null, new Size(10, 10));

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
            var bitmap = GraphicsOutput.DrawBitmap(robot.Grid, new Size(10, 10), 2f);

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
            var bitmap = GraphicsOutput.DrawBitmap(robot.Grid, new Size(10, 10), 2f, null, new Size(10, 10));

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
            var bitmap = GraphicsOutput.DrawBitmap(robot.Grid, new Size(10, 10), 1f, null, new Size(10, 10), Color.Red);

            // Assert
            Assert.Equal(Color.Red.R, bitmap.GetPixel(0, 0).R);
            Assert.Equal(Color.Red.R, bitmap.GetPixel(bitmap.Width - 1, 0).R);
            Assert.Equal(Color.Red.R, bitmap.GetPixel(0, bitmap.Height - 1).R);
            Assert.Equal(Color.Red.R, bitmap.GetPixel(bitmap.Width - 1, bitmap.Height - 1).R);
        }
    }
}
