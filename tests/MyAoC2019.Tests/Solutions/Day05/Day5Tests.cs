using Xunit;

namespace MyAoC2019.Tests.Solutions.Day05
{
    public class Day5Tests
    {
        [Fact]
        public void PartOne_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new MyAoC2019.Solutions.Day05.Day5
            {
                UnitTestMode = true
            };

            // Act
            var result = daySolution.GetResult(Part.One);

            // Assert
            Assert.Equal("7988899", result);
        }

        [Fact]
        public void PartTwo_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new MyAoC2019.Solutions.Day05.Day5
            {
                UnitTestMode = true
            };

            // Act
            var result = daySolution.GetResult(Part.Two);

            // Assert
            Assert.Equal("13758663", result);
        }
    }
}
