using Xunit;

namespace MyAoC2019.Tests.Solutions.Day4
{
    public class Day4Tests
    {
        [Fact]
        public void PartOne_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new MyAoC2019.Solutions.Day4.Day4();

            // Act
            var result = daySolution.GetResult(Part.One);

            // Assert
            Assert.Equal("1764", result);
        }

        [Fact]
        public void PartTwo_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new MyAoC2019.Solutions.Day4.Day4();

            // Act
            var result = daySolution.GetResult(Part.Two);

            // Assert
            Assert.Equal("1196", result);
        }
    }
}
