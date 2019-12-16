using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MyAoC2019.Tests.Solutions.Day14
{
    public class Day14Tests
    {
        [Fact]
        public void PartOne_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new MyAoC2019.Solutions.Day14.Day14
            {
                UnitTestMode = true
            };

            // Act
            var result = daySolution.GetResult(Part.One);

            // Assert
            Assert.Equal("371695", result);
        }

        [Fact]
        public void PartTwo_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new MyAoC2019.Solutions.Day14.Day14
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
