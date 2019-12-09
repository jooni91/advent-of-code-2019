using Xunit;

namespace MyAoC2019.Tests.Solutions.Day9
{
    public class Day9Tests
    {
        [Fact]
        public void PartOne_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new MyAoC2019.Solutions.Day9.Day9
            {
                UnitTestMode = true
            };

            // Act
            var result = daySolution.GetResult(Part.One);

            // Assert
            Assert.Equal("4288078517", result);
        }

        [Fact]
        public void PartTwo_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new MyAoC2019.Solutions.Day9.Day9
            {
                UnitTestMode = true
            };

            // Act
            var result = daySolution.GetResult(Part.Two);

            // Assert
            Assert.Equal("69256", result);
        }
    }
}
