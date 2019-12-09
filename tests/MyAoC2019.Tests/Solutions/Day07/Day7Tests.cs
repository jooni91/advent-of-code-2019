using Xunit;

namespace MyAoC2019.Tests.Solutions.Day07
{
    public class Day7Tests
    {
        [Fact]
        public void PartOne_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new MyAoC2019.Solutions.Day07.Day7();

            // Act
            var result = daySolution.GetResult(Part.One);

            // Assert
            Assert.Equal("17406", result);
        }

        [Fact]
        public void PartTwo_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new MyAoC2019.Solutions.Day07.Day7();

            // Act
            var result = daySolution.GetResult(Part.Two);

            // Assert
            Assert.Equal("1047153", result);
        }
    }
}
