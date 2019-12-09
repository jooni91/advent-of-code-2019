using Xunit;

namespace MyAoC2019.Tests.Solutions.Day01
{
    public class Day1Tests
    {
        [Fact]
        public void PartOne_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new MyAoC2019.Solutions.Day01.Day1();

            // Act
            var result = daySolution.GetResult(Part.One);

            // Assert
            Assert.Equal("3420719", result);
        }

        [Fact]
        public void PartTwo_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new MyAoC2019.Solutions.Day01.Day1();

            // Act
            var result = daySolution.GetResult(Part.Two);

            // Assert
            Assert.Equal("5128195", result);
        }
    }
}
