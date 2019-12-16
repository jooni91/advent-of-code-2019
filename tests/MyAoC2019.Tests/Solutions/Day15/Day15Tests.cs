using Xunit;

namespace MyAoC2019.Tests.Solutions.Day15
{
    public class Day15Tests
    {
        [Fact]
        public void PartOne_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new MyAoC2019.Solutions.Day15.Day15
            {
                UnitTestMode = true
            };

            // Act
            var result = daySolution.GetResult(Part.One);

            // Assert
            Assert.Equal("0", result);
        }

        //[Fact]
        //public void PartTwo_ShouldReturn_ExpectedValue()
        //{
        //    // Arrange
        //    var daySolution = new MyAoC2019.Solutions.Day09.Day9
        //    {
        //        UnitTestMode = true
        //    };

        //    // Act
        //    var result = daySolution.GetResult(Part.Two);

        //    // Assert
        //    Assert.Equal("69256", result);
        //}
    }
}
