using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyAoC2019.Utilities;
using Xunit;

namespace MyAoC2019.Tests.Utilities
{
    public class MathExtensionsTests
    {
        [Theory]
        [ClassData(typeof(GeneratePermutationsTestData))]
        public void GeneratePermutations_ShouldReturn_ExpectedResults<T>(T[] objects, T[][] expectedResults)
        {
            // Arrange


            // Act
            var result = MathExtensions.GeneratePermutations(objects, null).ToArray();

            // Assert
            Assert.Equal(expectedResults, result);
        }

        [Fact]
        public void GeneratePermutations_ShouldReturn_ExpectedCount()
        {
            // Arrange
            var objects = new int[] { 0, 1, 2, 3, 4, 5 };

            // Act
            var result = MathExtensions.GeneratePermutations(objects, null).Count;

            // Assert
            Assert.Equal(720, result);
        }

        [Fact]
        public void GeneratePermutations_WhenLimiting_ShouldReturn_ExpectedCount()
        {
            // Arrange
            var objects = new int[] { 0, 1, 2, 3, 4, 5 };

            // Act
            var result = MathExtensions.GeneratePermutations(objects, 2).Count;

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void GeneratePermutations_ShouldThrow_ArgumentOutOfRangeException()
        {
            // Arrange
            var objects = new int[257];

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => MathExtensions.GeneratePermutations(objects, null));
        }
    }

    public class GeneratePermutationsTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new int[] { 1, 0 }, new int[][] { new int[] { 0, 1 }, new int[] { 1, 0 } } };
            yield return new object[] 
            { 
                new int[] { 0, 1, 2 }, new int[][] 
                { 
                    new int[] { 2, 0, 1 }, new int[] { 2, 1, 0 }, new int[] { 1, 2, 0 }, 
                    new int[] { 0, 2, 1 }, new int[] { 1, 0, 2 }, new int[] { 0, 1, 2 }
                } 
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
