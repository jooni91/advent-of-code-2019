using Xunit;

namespace MyAoC2019.Tests.Solutions.Day6
{
    public class Day6Tests
    {
        [Fact]
        public void PartOne_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new MyAoC2019.Solutions.Day6.Day6();

            // Act
            var result = daySolution.GetResult(Part.One);

            // Assert
            Assert.Equal("333679", result);
        }

        [Fact]
        public void PartTwo_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new MyAoC2019.Solutions.Day6.Day6();

            // Act
            var result = daySolution.GetResult(Part.Two);

            // Assert
            Assert.Equal("370", result);
        }
    }
}
