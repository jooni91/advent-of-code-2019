using Xunit;

namespace MyAoC2019.Tests.Solutions.Day08
{
    public class Day8Tests
    {
        [Fact]
        public void PartOne_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new MyAoC2019.Solutions.Day08.Day8();

            // Act
            var result = daySolution.GetResult(Part.One);

            // Assert
            Assert.Equal("2032", result);
        }

        [Fact]
        public void PartTwo_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new MyAoC2019.Solutions.Day08.Day8
            {
                UnitTestMode = true
            };

            // Act
            var result = daySolution.GetResult(Part.Two);

            // Assert
            Assert.Equal("56", result);
        }
    }
}
